using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockMechanism : InteractibleObject
{
    [SerializeField, Tooltip("Animator du mesh")]
    private Animator m_meshAnimator;
    private BoxCollider m_col;

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

    [SerializeField, Tooltip("Son du mécanisme")]
    private SoundEvent m_mechanismeSound;

    private void Awake()
    {
        m_col = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        if (m_isLockOnStart) m_col.enabled = false;
    }

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
        
        GameManager.Instance.m_playerCtrl.AnimOpen();
        m_isPush = true;
        m_col.enabled = false;
        m_mechanismeSound.Play();
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
        m_col.enabled = !m_isLockOnStart;
        m_isLock = m_isLockOnStart;
        m_meshAnimator.ResetTrigger(m_animatorPushHash);
        m_meshAnimator.SetTrigger(m_animatorResetHash);
    }
}
