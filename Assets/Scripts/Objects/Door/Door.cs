using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractible
{
    public float m_cooldown = 0.5f;
    public List<Renderer> m_objectRendererToShine;
    private bool m_shinning = false;
    private int m_idShinning = Shader.PropertyToID("_shinning");

    public float Cooldown { get => m_cooldown; set => m_cooldown = value; }
    public List<Renderer> ObjectRendererToShine => m_objectRendererToShine;
    public bool Shinning { get => m_shinning; set => m_shinning = value; }
    public int IdShinning { get => m_idShinning; set => m_idShinning = value; }

    private bool m_isOpen = false;
    [SerializeField, Tooltip("Animator du Mesh")]
    private Animator m_animator;

    private int m_closeAnimator = Animator.StringToHash("close");
    private int m_OpenRightAnimator = Animator.StringToHash("openRight");
    private int m_OpenLeftAnimator = Animator.StringToHash("openLeft");

    public void Interact()
    {
        Debug.Log($"Intéraction de {gameObject.name}");
    }

    public void Shine()
    {
        if (m_shinning) return;
        
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

    /// <summary>
    /// Ouverture/Fermueture de la porte, s'ouvre par défaut du côté gauche
    /// </summary>
    /// <param name="m_left">Booléen qui défini si la porte s'ouvre à gauche ou à droite, ouverture à gauche par défaut</param>
    public void Toggle(bool m_left = true)
    {
        if (m_isOpen)
        {
            m_isOpen = false;
            m_animator.SetTrigger(m_closeAnimator);
            return;
        }
        m_isOpen = true;
        if (m_left)
        {
            m_animator.SetTrigger(m_OpenLeftAnimator);
            return;
        }
        m_animator.SetTrigger(m_OpenRightAnimator);
    }
}
