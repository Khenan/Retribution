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

    private void OnEnable()
    {
        m_animator = GetComponent<Animator>();
        m_listenEvent.m_event += Open;
    }
    
    private void OnDisable()
    {
        m_listenEvent.m_event -= Open;
    }

    private void Open()
    {
        m_animator.SetTrigger(m_animatorOpen);
    }
    
    public void Reset()
    {
        m_animator.SetTrigger(m_animatorReset);
    }
}
