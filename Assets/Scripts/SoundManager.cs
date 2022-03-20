using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void Play(SoundEvent m_soundEvent)
    {
        if (!m_soundEvent)
        {
            Debug.Log("Le son n'est pas enregistré dans le SoundManager");
            return;
        }
        m_soundEvent.Play();
    }

    public void Stop(SoundEvent m_soundEvent)
    {
        m_soundEvent.Stop();
    }

    protected override string GetSingletonName()
    {
        return "SoundManager";
    }
}
