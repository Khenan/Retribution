using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockFlapper : MonoBehaviour
{
    private Animator m_animator;

    private int m_animatorOpen = Animator.StringToHash("open");
    private int m_animatorReset = Animator.StringToHash("reset");
    
    [SerializeField, Tooltip("Evenement à écouter")]
    private Event m_listenEvent;
    [SerializeField, Tooltip("Evenement à écouter pour restart")]
    private Event m_listenEventRestart;

    [SerializeField, Tooltip("Son du clapet")]
    private SoundEvent m_soundEventFlapper;

    private void OnEnable()
    {
        m_animator = GetComponent<Animator>();
        m_listenEvent.m_event += Open;
        m_listenEventRestart.m_event += Reset;
    }
    
    private void OnDisable()
    {
        m_listenEvent.m_event -= Open;
        m_listenEventRestart.m_event -= Reset;
    }

    private void Open()
    {
        StartCoroutine(OpenCoroutine());
    }
    
    public void Reset()
    {
        m_animator.ResetTrigger(m_animatorOpen);
        m_animator.SetTrigger(m_animatorReset);
    }
    IEnumerator OpenCoroutine()
    {
        yield return new WaitForSeconds(2f);
        m_soundEventFlapper.Play();
        m_animator.SetTrigger(m_animatorOpen);
    }
}
