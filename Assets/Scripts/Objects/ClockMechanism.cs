using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockMechanism : InteractibleObject
{
    [SerializeField, Tooltip("Animator du mesh")]
    private Animator m_meshAnimator;

    private int m_animatorPushHash = Animator.StringToHash("push");
    private int m_animatorResetHash = Animator.StringToHash("reset");

    private bool m_isPush = false;
    [SerializeField, Tooltip("Le bouton est-t-il bloqué au start de l'énigme")]
    private bool m_isLockOnStart = false;
    private bool m_isLock = false;
    
    [SerializeField, Tooltip("Evenement à appeler")]
    private Event m_callEvent;
    [SerializeField, Tooltip("Evenement à écouter")]
    private Event m_listenEvent;
    [SerializeField, Tooltip("Evenement à écouter pour restart")]
    private Event m_listenEventRestart;

    private void OnEnable()
    {
        m_listenEventRestart.m_event += Reset;
        if (m_listenEvent == null) return;
        m_listenEvent.m_event += Handle;
    }
    
    private void OnDisable()
    {
        m_listenEventRestart.m_event -= Reset;
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
        m_meshAnimator.ResetTrigger(m_animatorResetHash);
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

    private void Reset()
    {
        m_isPush = false;
        m_isLock = m_isLockOnStart;
        m_meshAnimator.ResetTrigger(m_animatorPushHash);
        m_meshAnimator.SetTrigger(m_animatorResetHash);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.J)) 
            Debug.Log(m_isPush);
    }
}
