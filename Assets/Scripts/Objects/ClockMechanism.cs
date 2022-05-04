using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockMechanism : InteractibleObject
{
    [SerializeField, Tooltip("Animator du mesh")]
    private Animator m_meshAnimator;

    private int m_animatorPushHash = Animator.StringToHash("push");

    private bool m_isPush = false;
    [SerializeField, Tooltip("Le bouton est-t-il bloqué au start de l'énigme")]
    private bool m_isLockOnStart = false;
    private bool m_isLock = false;
    
    [SerializeField, Tooltip("Evenement à appeler")]
    private Event m_callEvent;
    [SerializeField, Tooltip("Evenement à écouter")]
    private Event m_listenEvent;

    private void OnEnable()
    {
        if (m_listenEvent == null) return;
        m_listenEvent.m_event += Handle;
    }
    
    private void OnDisable()
    {
        if (m_listenEvent == null) return;
        m_listenEvent.m_event -= Handle;
    }

    private void Handle()
    {
        m_isLock = false;
    }

    public override void Interact()
    {
        if (m_isPush || m_isLock) return;
        m_isPush = true;
        m_meshAnimator.SetTrigger(m_animatorPushHash);
        PushButton();
    }

    public override void Shine()
    {
        if (m_isPush || m_isLock) return;
        base.Shine();
    }

    private void PushButton()
    {
        Debug.Log("Button is Push");
        if(m_callEvent != null) m_callEvent.Raise();
    }

    public void Reset()
    {
        m_isLock = m_isLockOnStart;
    }
}
