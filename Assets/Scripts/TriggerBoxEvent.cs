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

    [SerializeField, Tooltip("Le trigger doit se lancer qu'une seule fois")]
    private bool m_once = false;

    public Collider m_col = null;

    private void Awake()
    {
        m_col = GetComponent<Collider>();
        m_col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_eventsToCall.Count == 0) return;
        foreach (Event e in m_eventsToCall)
        {
            e.Raise();
        }

        if (m_once) GetComponent<BoxCollider>().enabled = false;
    }

    public void Reset()
    {
        GetComponent<BoxCollider>().enabled = true;
    }
}
