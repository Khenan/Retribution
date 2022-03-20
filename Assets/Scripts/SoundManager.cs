using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField, Tooltip("Audio Walk")]
    private StudioEventEmitter m_audioWalk;

    private void Start()
    {
        m_audioWalk.Play();
    }

    protected override string GetSingletonName()
    {
        return "SoundManager";
    }

    public void Play(StudioEventEmitter p_event)
    {
        p_event.Play();
    }
}
