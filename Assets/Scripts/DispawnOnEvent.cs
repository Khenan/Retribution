using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispawnOnEvent : MonoBehaviour
{
    [SerializeField, Tooltip("Event à écouter pour dispawn")] private Event m_eventToDispawn;
    [SerializeField, Tooltip("Temps avant de disparaitre")] private float m_timeToDispawn;

    private void OnEnable()
    {
        m_eventToDispawn.m_event += Dispawn;
    }
    private void OnDisable()
    {
        m_eventToDispawn.m_event -= Dispawn;
    }

    private void Dispawn()
    {
        StartCoroutine(DispawnCoroutine());
    }

    IEnumerator DispawnCoroutine()
    {
        yield return new WaitForSeconds(m_timeToDispawn);
        gameObject.SetActive(false);
    }
}
