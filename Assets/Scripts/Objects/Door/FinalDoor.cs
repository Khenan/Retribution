using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class FinalDoor : MonoBehaviour
{
    [SerializeField, Tooltip("Event à écouter pour s'ouvrir")] private Event m_EventToListen;
    [SerializeField, Tooltip("Animator Porte Gauche")] private Animator m_leftAnimator;
    [SerializeField, Tooltip("Animator Porte Droite")] private Animator m_rightAnimator;
    [SerializeField, Tooltip("Son Porte Final")] private SoundEvent m_soundEvent;
    private void OnEnable() { m_EventToListen.m_event += Handle; }
    private void OnDisable() { m_EventToListen.m_event -= Handle; }
    private void Handle()
    {
        m_leftAnimator.SetTrigger("open");
        m_rightAnimator.SetTrigger("open");
        m_soundEvent.m_event.Play();
    }
}
