using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractible
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
    private List<Renderer> m_objectRendererToShine;
    private bool m_shining = false;
    private int m_idFresnelBlend = Shader.PropertyToID("_fresnelBlend");

    public float Cooldown { get => m_cooldownOff; set => m_cooldownOff = value; }
    public List<Renderer> ObjectRendererToShine => m_objectRendererToShine;
    public bool Shining { get => m_shining; set => m_shining = value; }

    [HideInInspector]
    public bool m_isOpen = false;
    [HideInInspector]
    public bool m_isLock = false;
    [SerializeField, Tooltip("Animator du Mesh")]
    private Animator m_animator;

    private readonly int m_closeAnimator = Animator.StringToHash("close");
    private readonly int m_openRightAnimator = Animator.StringToHash("openRight");
    private readonly int m_openLeftAnimator = Animator.StringToHash("openLeft");

    public void Interact()
    {
        
    }

    public void Shine()
    {
        if (m_isLock || m_isOpen) return;
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
    
    

    /// <summary>
    /// Ouverture/Fermueture de la porte, s'ouvre par défaut du côté gauche
    /// </summary>
    /// <param name="m_left">Booléen qui défini si la porte s'ouvre à gauche ou à droite, ouverture à gauche par défaut</param>
    public void Toggle(bool m_left = true)
    {
        m_animator.ResetTrigger(m_openRightAnimator);
        m_animator.ResetTrigger(m_openLeftAnimator);
        m_animator.ResetTrigger(m_closeAnimator);
        
        if (m_isOpen)
        {
            m_isOpen = false;
            m_animator.SetTrigger(m_closeAnimator);
            return;
        }
        if (m_isLock)
        {
            // Lancer une animation de porte fermée
            return;
        }
        m_isOpen = true;
        if (m_left)
        {
            m_animator.SetTrigger(m_openLeftAnimator);
            return;
        }
        m_animator.SetTrigger(m_openRightAnimator);
    }

    /// <summary>
    /// Fermeture de la porte, lance l'animation de fermeture
    /// </summary>
    public void Close()
    {
        m_animator.ResetTrigger(m_closeAnimator);
        m_isOpen = false;
        m_animator.SetTrigger(m_closeAnimator);
    }
}
