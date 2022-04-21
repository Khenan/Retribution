using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ChildCorridor : MonoBehaviour
{
    [SerializeField, Tooltip("Event à écouter pour s'ouvrir")]
    private List<Event> m_EventToListen = new List<Event>();

    private Rigidbody m_rb;
    private Vector3 m_initPos;
    private Quaternion m_initRot;

    private void OnEnable()
    {
        m_rb = GetComponent<Rigidbody>();
        m_initPos = transform.position;
        m_initRot = transform.rotation;
        
        foreach (Event e in m_EventToListen)
        {
            e.m_event += Run;
        }
    }
    private void OnDisable()
    {
        foreach (Event e in m_EventToListen)
        {
            e.m_event -= Run;
        }
    }

    private void Run()
    {
        m_rb.velocity = transform.forward;
    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
    }

    public void Reset()
    {
        m_rb.velocity = Vector3.zero;
        transform.position = m_initPos;
        transform.rotation = m_initRot;
        gameObject.SetActive(true);
    }
}
