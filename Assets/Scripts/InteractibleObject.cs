using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleObject : MonoBehaviour, IInteractible
{
    [SerializeField, Tooltip("Vitesse d'apparition de l'effet"), Range(1f, 1000f)]
    private float m_fadeInSpeed = 50f;
    [SerializeField, Tooltip("Vitesse de disparition de l'effet"), Range(0.01f, 0.1f)]
    private float m_fadeOutSpeed = 0.01f;
    [SerializeField, Tooltip("Cooldown de disparition de l'effet"), Range(0.01f, 0.1f)]
    private float m_cooldownOff = 0.01f;

    [SerializeField, Tooltip("Step d'apparition ou disparition du Fresnel"), Range(0.01f, 0.1f)]
    private float m_fresnelStep = 0.04f;
    [SerializeField, Tooltip("Objet à faire briller possédant le layer Interact")]
    private List<Renderer> m_objectRendererToShine;
    private int m_idFresnelBlend = Shader.PropertyToID("_fresnelBlend");

    public List<Renderer> ObjectRendererToShine => m_objectRendererToShine;
    public bool Takable { get; set; }

    public virtual void Interact()
    {
        
    }

    public virtual void Shine()
    {
        float fresnel = 0;
        foreach (Renderer rnd in m_objectRendererToShine)
        {
            foreach (Material mat in rnd.materials)
            {
                fresnel = mat.GetFloat(m_idFresnelBlend);
                fresnel -= m_fresnelStep * m_fadeInSpeed * Time.deltaTime;
                if (fresnel <= 0) fresnel = 0;
                mat.SetFloat(m_idFresnelBlend, fresnel);
            }
        }
        // Déclenchement du cooldown
        StopAllCoroutines();
        StartCoroutine(CooldownCoroutine());
    }

    public IEnumerator CooldownCoroutine()
    {
        yield return new WaitForSeconds(m_cooldownOff);
        StartCoroutine(FadeOutCoroutine());
    }
    IEnumerator FadeOutCoroutine()
    {
        yield return new WaitForSeconds(m_fadeOutSpeed);
        float fresnel = 0;
        foreach (Renderer rnd in m_objectRendererToShine)
        {
            foreach (Material mat in rnd.materials)
            {
                fresnel = mat.GetFloat(m_idFresnelBlend);
                fresnel += m_fresnelStep * 1.2f;
                if (fresnel >= 1) fresnel = 1;
                mat.SetFloat(m_idFresnelBlend, fresnel);
            }
        }
        if(fresnel < 1) StartCoroutine(FadeOutCoroutine());
    }
}
