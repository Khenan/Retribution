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

    private void Awake()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_eventsToCall.Count == 0) return;
        foreach (Event e in m_eventsToCall)
        {
            e.Raise();
        }
    }
}
