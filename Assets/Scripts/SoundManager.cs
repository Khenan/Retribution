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
    [SerializeField, Tooltip("Son de mort")]
    public SoundEvent m_playerDeathSound;
    [SerializeField, Tooltip("Son de toux")]
    public SoundEvent m_playerCoughSound;
    

    // ENIGME 1
    [Header("FIRST ENIGMA'S SOUNDS")]
    [Tooltip("Son du tableau à craie")]
    public SoundEvent m_chalkboardWriting;

    // MUSIC AMBIANT
    [Header("AMBIANT'S SOUNDS")]
    [SerializeField, Tooltip("Event pour lancer la music")] private Event m_playMusicEvent;
    [SerializeField, Tooltip("Event pour stoper la music")] private Event m_stopMusicEvent;
    [SerializeField, Tooltip("MusicAmbiant")] private SoundEvent m_ambiantMusic;
    
    private void OnEnable()
    {
        m_playMusicEvent.m_event += PlayMusic;
        m_stopMusicEvent.m_event += StopMusic;
    }
    private void OnDisable()
    {
        m_playMusicEvent.m_event -= PlayMusic;
        m_stopMusicEvent.m_event -= StopMusic;
    }

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
    private void PlayMusic()
    {
        m_ambiantMusic.Play();
    }

    private void StopMusic()
    {
        m_ambiantMusic.Stop();
    }

    protected override string GetSingletonName()
    {
        return "SoundManager";
    }
}
