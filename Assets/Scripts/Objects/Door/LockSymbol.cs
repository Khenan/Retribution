using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockSymbol : MonoBehaviour
{
    [SerializeField, Tooltip("Event à écouter pour disparaitre")] private Event m_eventToListen;
    private Animator m_animator;
    private int m_disappearAnim = Animator.StringToHash("disappear");

    private void Awake()
    {
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

    private void Handle()
    {
        m_animator.SetTrigger(m_disappearAnim);
        Destroy(gameObject, 1);
    }
}
