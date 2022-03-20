using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractible
{
    public float m_cooldown = 0.5f;
    public List<Renderer> m_objectRendererToShine;
    private bool m_shining = false;
    private int m_idShining = Shader.PropertyToID("_shining");

    public float Cooldown { get => m_cooldown; set => m_cooldown = value; }
    public List<Renderer> ObjectRendererToShine => m_objectRendererToShine;
    public bool Shinning { get => m_shining; set => m_shining = value; }
    public int IdShinning { get => m_idShining; set => m_idShining = value; }

    private bool m_isOpen = false;
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
        if (m_shining) return;
        
        m_shining = true;
        // Déclanchement du shining
        foreach (Renderer rnd in m_objectRendererToShine)
        {
            foreach (Material mat in rnd.materials)
            {
                mat.SetFloat(m_idShining, 1);
            }
        }
        // Déclanchement du cooldown
        StartCoroutine(CooldownCoroutine());
    }

    public IEnumerator CooldownCoroutine()
    {
        yield return new WaitForSeconds(m_cooldown);
        m_shining = false;
        // Arrêt du shining
        foreach (Renderer rnd in m_objectRendererToShine)
        {
            foreach (Material mat in rnd.materials)
            {
                mat.SetFloat(m_idShining, 0);
            }
        }
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
        m_isOpen = false;
        m_animator.SetTrigger(m_closeAnimator);
    }
}
