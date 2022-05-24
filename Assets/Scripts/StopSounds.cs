using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopSounds : MonoBehaviour
{
    [SerializeField, Tooltip("Event pour stoper les son de feux")] private Event m_stopFireSoundEvent;

    [SerializeField, Tooltip("List des sons à stoper")]
    private List<Transform> m_Sounds;

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
        foreach (Transform t in m_Sounds)
        {
            t.gameObject.SetActive(false);
        }
    }
}
