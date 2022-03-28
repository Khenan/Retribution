using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pupitre : MonoBehaviour, IInteractible
{
    public float m_cooldown = 0.5f;
    
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
    public List<Renderer> ObjectRendererToShine => m_objectRendererToShine;
    public float Cooldown { get => m_cooldown; set => m_cooldown = value; }
    public bool Shining { get => m_shining; set => m_shining = value; }

    public bool m_isOpen = false;
    [SerializeField, Tooltip("Animator du Mesh")]
    private Animator m_animator;

    private readonly int m_closeAnimator = Animator.StringToHash("close");
    private readonly int m_openAnimator = Animator.StringToHash("open");

    private EnigmePupitre m_enigmePupitre;
    [SerializeField, Tooltip("Numéro du pupitre"), Range(0, 10)]
    private int m_numPupitre = 0;

    private void Awake()
    {
        m_enigmePupitre = FindObjectOfType<EnigmePupitre>();
    }

    private void OnEnable()
    {
        EnigmePupitre.Instance.m_close += Close;
    }
    private void OnDisable()
    {
        EnigmePupitre.Instance.m_close -= Close;
    }

    public void Interact()
    {
        if (m_isOpen) return;
        if (m_enigmePupitre.CheckPupitre(m_numPupitre))
        {
            Open();
        }
    }

    public void Shine()
    {
        if (m_isOpen) return;
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
    
    private void Open()
    {
        m_isOpen = true;
        print("Open !");
        m_animator.SetTrigger(m_openAnimator);
    }
    private void Close()
    {
        if (!m_isOpen) return;
        m_isOpen = false;
        print("Close !");
        m_animator.SetTrigger(m_closeAnimator);
    }
}
