using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class FinalDoor : MonoBehaviour
{
    [SerializeField, Tooltip("Event à écouter pour s'ouvrir")] private Event m_EventToListen_open;
    [SerializeField, Tooltip("Event à écouter pour se fermer")] private Event m_EventToListen_close;
    [SerializeField, Tooltip("Event à écouter pour se fermer lentement")] private Event m_EventToListen_slowClose;
    [SerializeField, Tooltip("Animator Porte Gauche")] private Animator m_leftAnimator;
    [SerializeField, Tooltip("Animator Porte Droite")] private Animator m_rightAnimator;
    [SerializeField, Tooltip("Son Porte Final Open Slow")] private SoundEvent m_soundEventOpen;
    [SerializeField, Tooltip("Son Porte Final Close Slow")] private SoundEvent m_soundEventClose;

    private void OnEnable()
    {
        m_EventToListen_open.m_event += Open;
        m_EventToListen_close.m_event += Close;
        m_EventToListen_slowClose.m_event += SlowClose;
    }

    private void OnDisable()
    {
        m_EventToListen_open.m_event -= Open;
        m_EventToListen_close.m_event -= Close;
        m_EventToListen_slowClose.m_event -= SlowClose;
    }
    private void Open()
    {
        m_leftAnimator.ResetTrigger("open");
        m_rightAnimator.ResetTrigger("open");
        m_leftAnimator.SetTrigger("open");
        m_rightAnimator.SetTrigger("open");
        m_soundEventOpen.m_event.Stop();
        m_soundEventOpen.m_event.Play();
    }
    public void Close()
    {
        m_leftAnimator.ResetTrigger("close");
        m_rightAnimator.ResetTrigger("close");
        m_leftAnimator.SetTrigger("close");
        m_rightAnimator.SetTrigger("close");
        //if(m_soundEventFast != null )m_soundEventFast.m_event.Stop();
        //if(m_soundEventFast != null )m_soundEventFast.m_event.Play();
    }
    private void SlowClose()
    {
        m_leftAnimator.ResetTrigger("slowClose");
        m_rightAnimator.ResetTrigger("slowClose");
        m_leftAnimator.SetTrigger("slowClose");
        m_rightAnimator.SetTrigger("slowClose");
        Debug.Log("Sounds PLAY !!!");
        m_soundEventClose.m_event.Stop();
        m_soundEventClose.m_event.Play();
    }
}
