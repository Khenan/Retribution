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
    private int m_idFresnelBlend = Shader.PropertyToID("_fresnelBlend");
    
    [SerializeField, Tooltip("L'objet peut-il être prit ?")]
    private bool m_takable = false;

    public bool m_isTake = false;

    public List<Renderer> ObjectRendererToShine => m_objectRendererToShine;
    public bool Shining { get; set; }
    public bool Takable { get => m_takable; set => m_takable = value; }

    private Vector3 m_initPos;
    private Transform m_initParent;
    private Quaternion m_initQuaternion;
    
    
    [SerializeField, Tooltip("Animator du Mesh")]
    private Animator m_animator;
    // Animation du totem quand il se brise
    // private readonly int m_breakAnimator = Animator.StringToHash("break");

    private void OnEnable()
    {
        var tr = transform;
        m_initPos = tr.position;
        m_initParent = tr.parent;
        m_initQuaternion = tr.rotation;
        Debug.Log(m_initPos.x);
    }

    public void Reset()
    {
        m_isTake = false;
        m_takable = false;
        
        Debug.Log(m_initPos.x);
        var tr = transform;
        tr.position = m_initPos;
        tr.SetParent(m_initParent);
        tr.rotation = m_initQuaternion;
        
        gameObject.layer = LayerMask.NameToLayer("Interact");
        if (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.layer = LayerMask.NameToLayer("Interact");
            }
        }
        GameManager.Instance.m_playerCtrl.m_interactionController.Drop();
    }

    public void Interact()
    {
        gameObject.layer = LayerMask.NameToLayer("Overlay");
        m_isTake = true;
        if (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.layer = LayerMask.NameToLayer("Overlay");
            }
        }
    }

    public void Shine()
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
