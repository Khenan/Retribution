using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

[RequireComponent(typeof(StudioEventEmitter))]
public class SoundEvent : MonoBehaviour
{
    public StudioEventEmitter m_event;

    private void Awake()
    {
        if(m_event == null) m_event = GetComponent<StudioEventEmitter>();
    }

    public void Play()
    {
        m_event.Play();
    }
    public void Stop()
    {
        m_event.Stop();
    }
}
