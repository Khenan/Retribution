using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractibleObject
{
    [SerializeField, Tooltip("La porte est ouverte à gauche au début du jeu")]
    private bool m_isOpenLeftOnStart = false;
    [SerializeField, Tooltip("La porte est ouverte à droite au début du jeu")]
    private bool m_isOpenRightOnStart = false;
    

    [HideInInspector]
    public bool m_isOpen = false;
    [HideInInspector]
    public bool m_isLock = false;
    [SerializeField, Tooltip("Animator du Mesh")]
    private Animator m_animator;

    private readonly int m_closeAnimator = Animator.StringToHash("close");
    private readonly int m_openRightAnimator = Animator.StringToHash("openRight");
    private readonly int m_openLeftAnimator = Animator.StringToHash("openLeft");

    [SerializeField, Tooltip("Event à écouter pour se fermer")]
    private List<Event> m_EventToListen_Open = new List<Event>();
    [SerializeField, Tooltip("Event à écouter pour s'ouvrir")]
    private List<Event> m_EventToListen_Close = new List<Event>();
    [SerializeField, Tooltip("Event à lire quand la porte s'ouvre")]
    private List<Event> m_EventToRead_Open = new List<Event>();

    private bool eventLaunched = false;

    private void OnEnable()
    {
        foreach (Event e in m_EventToListen_Open)
        {
            e.m_event += OpenLeft;
        }
        foreach (Event e in m_EventToListen_Close)
        {
            e.m_event += Close;
        }
    }
    private void OnDisable()
    {
        foreach (Event e in m_EventToListen_Open)
        {
            e.m_event -= OpenLeft;
        }
        foreach (Event e in m_EventToListen_Close)
        {
            e.m_event -= Close;
        }
    }

    private void Start()
    {
        if (!m_isOpenLeftOnStart && !m_isOpenRightOnStart) return;
        
        if (m_isOpenLeftOnStart) Toggle();
        else if (m_isOpenRightOnStart) Toggle(false);
    }

    

    public override void Shine()
    {
        if (m_isLock || m_isOpen) return;
        base.Shine();
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
            LaunchAnimLock();
            return;
        }
        m_isOpen = true;
        if (m_left)
        {
            LaunchEvent();
            m_animator.SetTrigger(m_openLeftAnimator);
            return;
        }
        LaunchEvent();
        m_animator.SetTrigger(m_openRightAnimator);
    }

    /// <summary>
    /// Fermeture de la porte, lance l'animation de fermeture
    /// </summary>
    public void Close()
    {
        m_isLock = true;
        m_animator.ResetTrigger(m_closeAnimator);
        m_isOpen = false;
        m_animator.SetTrigger(m_closeAnimator);
    }
    public void OpenLeft()
    {
        m_animator.ResetTrigger(m_openRightAnimator);
        m_animator.ResetTrigger(m_openLeftAnimator);
        m_animator.ResetTrigger(m_closeAnimator);

        if (m_isOpen) return;
        
        m_isOpen = true;
        m_animator.SetTrigger(m_openLeftAnimator);
        LaunchEvent();
    }

    private void LaunchAnimLock()
    {
        // Lancer une animation de porte fermée
    }

    private void LaunchEvent()
    {
        if (eventLaunched) return;
        eventLaunched = true;
        foreach (Event e in m_EventToRead_Open)
        {
            e.Raise();
        }
    }
}
