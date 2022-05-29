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
        m_closeFinalDoorSound.Stop();
        m_closeFinalDoorSound.Play();
        foreach (SoundEvent s in m_Sounds)
        {
            if(s != null) s.Stop();
        }
    }

    IEnumerator SoundCoroutine()
    {
        yield return new WaitForSeconds(1);
        m_laughSound.Stop();
        m_laughSound.Play();
    }
}
