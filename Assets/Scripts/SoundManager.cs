using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class SoundManager : Singleton<SoundManager>
{
    // PLAYER
    [Header("PLAYER'S SOUNDS")]
    [Tooltip("Son de la marche du joueur avec le bois qui grince")]
    public SoundEvent m_PlayerWalkTop;
    [Tooltip("Son de la marche du joueur au sol en bas")]
    public SoundEvent m_PlayerWalkBottom;
    
    // DOOR
    [Header("DOOR'S SOUNDS")]
    [Tooltip("Son de la porte qui s'ouvre")]
    public SoundEvent m_DoorOpenning;
    [Tooltip("Son de la porte qui se ferme")]
    public SoundEvent m_DoorClosing;

    public void Play(StudioEventEmitter m_sound)
    {
        if (!m_sound)
        {
            Debug.Log("Le son n'est pas enregistré dans le SoundManager");
            return;
        }
        m_sound.Play();
    }

    public void Stop(StudioEventEmitter m_sound)
    {
        m_sound.Stop();
    }

    protected override string GetSingletonName()
    {
        return "SoundManager";
    }
    [System.Serializable]
    public struct SoundEvent
    {
        public StudioEventEmitter sound;
        // [HideInInspector]
        public bool isPlaying;
    }
}
