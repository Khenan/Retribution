using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pupitre : MonoBehaviour, IInteractible
{
    public float m_cooldown = 0.5f;
    public List<Renderer> m_objectRendererToShine;
    private bool m_shinning = false;
    private int m_idShinning = Shader.PropertyToID("_shinning");

    public List<Renderer> ObjectRendererToShine => m_objectRendererToShine;
    public float Cooldown { get => m_cooldown; set => m_cooldown = value; }
    public bool Shinning { get => m_shinning; set => m_shinning = value; }
    public int IdShinning { get => m_idShinning; set => m_idShinning = value; }

    private bool m_isOpen = false;
    [SerializeField, Tooltip("Animator du Mesh")]
    private Animator m_animator;

    private readonly int m_closeAnimator = Animator.StringToHash("close");
    private readonly int m_openAnimator = Animator.StringToHash("open");

    public void Interact()
    {
        Open();
    }

    public void Shine()
    {
        if (m_shinning || m_isOpen) return;
        
        m_shinning = true;
        foreach (Renderer rnd in m_objectRendererToShine)
            rnd.material.SetFloat(m_idShinning, 1);
        
        StartCoroutine(CooldownCoroutine());
    }

    public IEnumerator CooldownCoroutine()
    {
        yield return new WaitForSeconds(m_cooldown);
        m_shinning = false;
        foreach (Renderer rnd in m_objectRendererToShine)
            rnd.material.SetFloat(m_idShinning, 0);
    }
    
    private void Open()
    {
        if (m_isOpen) return;
        m_isOpen = true;
        m_animator.SetTrigger(m_openAnimator);
    }
    public void Close()
    {
        m_isOpen = false;
        m_animator.SetTrigger(m_closeAnimator);
    }
}
