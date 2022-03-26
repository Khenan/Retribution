using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pupitre : MonoBehaviour, IInteractible
{
    public float m_cooldown = 0.5f;
    public List<Renderer> m_objectRendererToShine;
    private bool m_shining = false;
    private int m_idShining = Shader.PropertyToID("_shining");

    public List<Renderer> ObjectRendererToShine => m_objectRendererToShine;
    public float Cooldown { get => m_cooldown; set => m_cooldown = value; }
    public bool Shining { get => m_shining; set => m_shining = value; }
    public int IdShining { get => m_idShining; set => m_idShining = value; }

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
        if (m_shining || m_isOpen) return;
        
        m_shining = true;
        foreach (Renderer rnd in m_objectRendererToShine)
            rnd.material.SetFloat(m_idShining, 1);
        
        StartCoroutine(CooldownCoroutine());
    }

    public IEnumerator CooldownCoroutine()
    {
        yield return new WaitForSeconds(m_cooldown);
        m_shining = false;
        foreach (Renderer rnd in m_objectRendererToShine)
            rnd.material.SetFloat(m_idShining, 0);
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
