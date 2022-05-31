using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopSounds : MonoBehaviour
{
    [SerializeField, Tooltip("Event pour stoper les son de feux")] private Event m_stopFireSoundEvent;

    [SerializeField, Tooltip("List des sons à stoper")]
    private List<SoundEvent> m_Sounds;
    
    [SerializeField, Tooltip("Son du rire")] private SoundEvent m_laughSound;
    [SerializeField, Tooltip("Claquement de la porte finale")] private SoundEvent m_closeFinalDoorSound;
    public Animator m_leftAnimator;
    public Animator m_rightAnimator;

    private WaitForSeconds m_wait1;

    private void Awake()
    {
        m_wait1 = new WaitForSeconds(1);
    }

    private void OnEnable()
    {
        m_stopFireSoundEvent.m_event += Stop;
    }
    private void OnDisable()
    {
        m_stopFireSoundEvent.m_event -= Stop;
    }

    private void Stop()
    {
        StartCoroutine(SoundCoroutine());
        UIManager.Instance.FadeOutFast();
        StartCoroutine(CreditsCoroutine());
        m_closeFinalDoorSound.Stop();
        m_closeFinalDoorSound.Play();
        m_leftAnimator.ResetTrigger("close");
        m_rightAnimator.ResetTrigger("close");
        m_leftAnimator.SetTrigger("close");
        m_rightAnimator.SetTrigger("close");
        foreach (SoundEvent s in m_Sounds)
        {
            if(s != null) s.Stop();
        }
    }

    IEnumerator CreditsCoroutine()
    {
        yield return m_wait1;
        GameManager.Instance.LockCursor(false);
        SceneManager.Instance.ChangeSceneDirect(2);
    }
    IEnumerator SoundCoroutine()
    {
        yield return m_wait1;
        m_laughSound.Stop();
        m_laughSound.Play();
    }
}
