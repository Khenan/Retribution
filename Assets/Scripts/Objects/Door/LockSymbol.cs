using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockSymbol : MonoBehaviour
{
    [SerializeField, Tooltip("Event à écouter pour disparaitre")] private Event m_eventToListen;

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
        Destroy(gameObject, 0.5f);
    }
}
