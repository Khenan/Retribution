using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

[RequireComponent(typeof(StudioEventEmitter))]
public class SoundEvent : MonoBehaviour
{
    private StudioEventEmitter m_event;
    private bool m_isPlaying = false;

    [Tooltip("Le son a le droit d'être joué même s'il est déjà lancé")]
    public bool m_allowPlayOnPlay = false;

    private void Awake()
    {
        m_event = GetComponent<StudioEventEmitter>();
    }

    public void Play()
    {
        if (!m_allowPlayOnPlay && m_isPlaying) return;
        Debug.Log("PLAY !");
        m_event.Play();
        m_isPlaying = true;
    }
    public void Stop()
    {
        if (!m_isPlaying) return;
        Debug.Log("STOP !");
        m_event.Stop();
        m_isPlaying = false;
    }
}
