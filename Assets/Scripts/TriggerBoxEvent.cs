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

    [SerializeField, Tooltip("BoxCollider Supplémentaire")]
    private List<BoxCollider> m_colliderSupplementaire;

    [HideInInspector]
    public Collider m_col = null;

    private void Awake()
    {
        m_col = GetComponent<Collider>();
        m_col.isTrigger = true;
        if (!m_eventsToPop) return;
        m_col.enabled = false;
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
        Debug.Log("Trigger touché !");
        if (m_eventsToCall.Count == 0) return;
        foreach (Event e in m_eventsToCall)
        {
            e.Raise();
        }

        if (!m_once) return;
        m_col.enabled = false;
        if (m_colliderSupplementaire.Count > 0) m_colliderSupplementaire.ForEach(c => c.enabled = false);
    }

    public void Reset()
    {
        m_col.enabled = true;
    }
}
