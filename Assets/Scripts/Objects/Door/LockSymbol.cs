using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockSymbol : MonoBehaviour
{
    public SoundEvent m_sound;
    [SerializeField, Tooltip("Event à écouter pour disparaitre")] private Event m_eventToListen;
    public Animator m_animator;
    public int m_disappearAnim = Animator.StringToHash("disappear");

    private void Awake()
    {
        m_sound = GetComponent<SoundEvent>();
        m_animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        if (m_eventToListen != null) m_eventToListen.m_event += Handle;
    }
    private void OnDisable()
    {
        if (m_eventToListen != null) m_eventToListen.m_event -= Handle;
    }

    public void Handle()
    {
        if(m_sound != null) m_sound.Play();
        if(m_animator != null) m_animator.SetTrigger(m_disappearAnim);
        Destroy(gameObject, 1);
    }
}
