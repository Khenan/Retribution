using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TriggerBoxEvent : MonoBehaviour
{
    [Header("Events")]
    [SerializeField, Tooltip("Events à appeler quand on rentre dans la boite")]
    private List<Event> m_eventsToCall = new List<Event>();

    [SerializeField, Tooltip("Events à lire pour s'activer")]
    private Event m_eventsToPop;

    [SerializeField, Tooltip("Le trigger doit se lancer qu'une seule fois")]
    private bool m_once = false;

    [HideInInspector]
    public Collider m_col = null;

    private void Awake()
    {
        m_col = GetComponent<Collider>();
        m_col.isTrigger = true;
    }

    private void OnEnable()
    {
        if (!m_eventsToPop) return;
        m_eventsToPop.m_event += Pop;
    }
    private void OnDisable()
    {
        if (!m_eventsToPop) return;
        m_eventsToPop.m_event -= Pop;
    }

    private void Pop()
    {
        m_col.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_eventsToCall.Count == 0) return;
        foreach (Event e in m_eventsToCall)
        {
            e.Raise();
        }

        if (m_once) m_col.enabled = false;
    }

    public void Reset()
    {
        m_col.enabled = true;
    }
}
