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
    [SerializeField, Tooltip("End")]
    public SoundEvent m_playerEndSound;
    

    // ENIGME 1
    [Header("FIRST ENIGMA'S SOUNDS")]
    [Tooltip("Son du tableau à craie")]
    public SoundEvent m_chalkboardWriting;

    // MUSIC AMBIANT
    [Header("AMBIANT'S SOUNDS")]
    [SerializeField, Tooltip("Event pour lancer la music ambiente")] private Event m_playAmbiantMusicEvent;
    [SerializeField, Tooltip("Event pour stoper la music ambiente")] private Event m_stopAmbiantMusicEvent;
    [SerializeField, Tooltip("MusicAmbiant")] private SoundEvent m_ambiantMusic;
    // MUSIC FINAL
    [Header("FINAL MUSIC'S SOUNDS")]
    [SerializeField, Tooltip("Event pour lancer la music finale")] private Event m_playFinalMusicEvent;
    [SerializeField, Tooltip("Event pour stoper la music finale")] private Event m_stopFinalMusicEvent;
    [SerializeField, Tooltip("Music Finale")] private SoundEvent m_finalMusic;
    
    private void OnEnable()
    {
        m_playAmbiantMusicEvent.m_event += PlayAmbiantMusic;
        m_stopAmbiantMusicEvent.m_event += StopAmbiantMusic;
        m_playFinalMusicEvent.m_event += PlayFinalMusic;
        m_stopFinalMusicEvent.m_event += StopFinalMusic;
    }
    private void OnDisable()
    {
        m_playAmbiantMusicEvent.m_event -= PlayAmbiantMusic;
        m_stopAmbiantMusicEvent.m_event -= StopAmbiantMusic;
        m_playFinalMusicEvent.m_event -= PlayFinalMusic;
        m_stopFinalMusicEvent.m_event -= StopFinalMusic;
    }

    public void Play(SoundEvent m_soundEvent)
    {
        if (!m_soundEvent)
        {
            Debug.LogWarning("Le son n'est pas enregistré dans le SoundManager");
            return;
        }
        m_soundEvent.Play();
    }

    public void Stop(SoundEvent m_soundEvent) { m_soundEvent.Stop(); }
    public void PlayAmbiantMusic() { m_ambiantMusic.Play(); }
    public void StopAmbiantMusic() { m_ambiantMusic.Stop(); }
    public void PlayFinalMusic()
    {
        StopAmbiantMusic();
        m_finalMusic.Play();
    }
    public void StopFinalMusic() { m_finalMusic.Stop(); }

    protected override string GetSingletonName() { return "SoundManager"; }
}
