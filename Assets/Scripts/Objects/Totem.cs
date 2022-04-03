using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : MonoBehaviour, IInteractible
{
    [SerializeField, Tooltip("Vitesse d'apparition de l'effet"), Range(1f, 1000f)]
    private float m_fadeInSpeed = 0.05f;
    [SerializeField, Tooltip("Vitesse de disparition de l'effet"), Range(0.01f, 0.1f)]
    private float m_fadeOutSpeed = 0.05f;
    [SerializeField, Tooltip("Cooldown de disparition de l'effet"), Range(0.01f, 0.1f)]
    private float m_cooldownOff = 0.05f;

    [SerializeField, Tooltip("Step d'apparition ou disparition du Fresnel"), Range(0.01f, 0.1f)]
    private float m_fresnelStep = 0.05f;
    [SerializeField, Tooltip("Objet à faire briller possédant le layer Interact")]
    private List<Renderer> m_objectRendererToShine = new List<Renderer>();
    private bool m_shining = false;
    private int m_idFresnelBlend = Shader.PropertyToID("_fresnelBlend");
    
    [SerializeField, Tooltip("L'objet peut-il être prit ?")]
    private bool m_takable = false;
    [SerializeField, Tooltip("L'objet est-il prit ?")]
    private bool m_isTake = false;

    public List<Renderer> ObjectRendererToShine => m_objectRendererToShine;
    public bool Shining { get; set; }
    public bool Takable { get => m_takable; set => m_takable = value; }
    
    
    [SerializeField, Tooltip("Animator du Mesh")]
    private Animator m_animator;
    // Animation du totem quand il se brise
    // private readonly int m_breakAnimator = Animator.StringToHash("break");

    public void Interact()
    {
        m_isTake = true;
        gameObject.layer = LayerMask.NameToLayer("Overlay");
    }

    public void Shine()
    {
        
        m_shining = true;
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
        m_shining = false;
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
        m_shining = false;
        if(fresnel < 1) StartCoroutine(FadeOutCoroutine());
    }
}
