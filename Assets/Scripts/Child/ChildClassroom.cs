using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ChildClassroom : MonoBehaviour
{
    [SerializeField, Tooltip("Transform de l'enfant")] private Transform m_child;
    [SerializeField, Tooltip("Temps que l'enfant reste")] private float m_standingTime;
    [SerializeField, Tooltip("Temps à regarder")] private float m_timeLookTarget = 3f;

    public float m_currentTimeLook = 0f;
    private float m_stepDecreaseTime = 0.01f;

    private bool m_isLock;

    private WaitForSeconds m_cooldownWait = new WaitForSeconds(0.2f);
    private WaitForSeconds m_resetWait = new WaitForSeconds(0.01f);

    private Coroutine m_cooldownCoroutine = null;
    private Coroutine m_loopResetCoroutine = null;

    [FormerlySerializedAs("m_finalSound")] [SerializeField, Tooltip("Son du finish")] private SoundEvent m_screamSound;
    [SerializeField, Tooltip("Event à lire")] private Event m_eventToRead;
    [SerializeField, Tooltip("Event à écouter pour se bloquer")] private Event m_event1ToLock;
    [SerializeField, Tooltip("Event à écouter pour se bloquer")] private Event m_event2ToLock;

    private void OnEnable()
    {
        if(m_event1ToLock != null) m_event1ToLock.m_event += Lock;
        if(m_event2ToLock != null) m_event2ToLock.m_event += Lock;
    }
    private void OnDisable()
    {
        if(m_event1ToLock != null) m_event1ToLock.m_event -= Lock;
        if(m_event2ToLock != null) m_event2ToLock.m_event -= Lock;
    }

    private void Lock()
    {
        m_isLock = true;
    }

    public void LookContinue(float p_timeValue)
    {
        if (m_isLock) return;
        m_currentTimeLook += p_timeValue;
        if (m_currentTimeLook >= m_timeLookTarget)
        {
            GameManager.Instance.m_playerCtrl.AnimJumpscare();
            m_isLock = true;
            m_screamSound.Play();
            m_eventToRead.Raise();
            StartCoroutine(PopCoroutine());
            m_currentTimeLook = m_timeLookTarget;
        }
        
        if (m_isLock) return;
        if(m_cooldownCoroutine != null) StopCoroutine(m_cooldownCoroutine);
        if(m_loopResetCoroutine != null) StopCoroutine(m_loopResetCoroutine);
        m_cooldownCoroutine = StartCoroutine(CooldownResetTimeCoroutine());
    }

    private void Pop()
    {
        m_child.GetComponent<Animator>().SetTrigger("pop");
    }

    IEnumerator PopCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        Pop();
    }
    IEnumerator CooldownResetTimeCoroutine()
    {
        yield return m_cooldownWait;
        if (m_isLock) yield break;
        m_loopResetCoroutine = StartCoroutine(LoopResetTimeCoroutine());
    }
    IEnumerator LoopResetTimeCoroutine()
    {
        yield return m_resetWait;
        if (m_isLock) yield break;
        m_currentTimeLook -= m_stepDecreaseTime;
        if (m_currentTimeLook <= 0) m_currentTimeLook = 0;
        if (m_currentTimeLook > 0) m_loopResetCoroutine = StartCoroutine(LoopResetTimeCoroutine());
    }

    IEnumerator Depop()
    {
        yield return new WaitForSeconds(m_standingTime);
        m_child.gameObject.SetActive(false);
    }

    public void Reset()
    {
        m_currentTimeLook = 0;
    }
}