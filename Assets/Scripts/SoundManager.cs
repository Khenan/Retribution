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
    

    // ENIGME 1
    [Header("FIRST ENIGMA'S SOUNDS")]
    [Tooltip("Son du tableau à craie")]
    public SoundEvent m_chalkboardWriting;

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
