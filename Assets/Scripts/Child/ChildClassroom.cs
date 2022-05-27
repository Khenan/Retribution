using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildClassroom : MonoBehaviour
{
    [SerializeField, Tooltip("Transform de l'enfant")]
    private Transform m_child;
    [SerializeField, Tooltip("Transform final de l'enfant")]
    private Transform m_finalTransform;

    private float m_timeLookTarget = 3f;
    public float m_currentTimeLook = 0f;
    private float m_stepDecreaseTime = 0.01f;

    private bool m_isLock;

    private WaitForSeconds m_cooldownWait = new WaitForSeconds(0.2f);
    private WaitForSeconds m_resetWait = new WaitForSeconds(0.01f);

    private Coroutine m_cooldownCoroutine = null;
    private Coroutine m_loopResetCoroutine = null;

    [SerializeField, Tooltip("Son du finish")]
    private SoundEvent m_finalSound;

    public void LookContinue(float p_timeValue)
    {
        Debug.Log("Je recois le trigger d'enfant");
        if (m_isLock) return;
        Debug.Log("Is look !");
        m_currentTimeLook += p_timeValue;
        if (m_currentTimeLook >= m_timeLookTarget)
        {
            Debug.Log("Terminé !");
            m_finalSound.Play();
            m_isLock = true;
            Pop();
            m_currentTimeLook = m_timeLookTarget;
        }
        
        if (m_isLock) return;
        if(m_cooldownCoroutine != null) StopCoroutine(m_cooldownCoroutine);
        if(m_loopResetCoroutine != null) StopCoroutine(m_loopResetCoroutine);
        m_cooldownCoroutine = StartCoroutine(CooldownResetTimeCoroutine());
    }

    private void Pop()
    {
        m_child.position = m_finalTransform.position;
        m_child.rotation = m_finalTransform.rotation;
        StartCoroutine(Depop());
    }

    IEnumerator CooldownResetTimeCoroutine()
    {
        yield return m_cooldownWait;
        if (m_isLock) yield break;
        Debug.Log("Reset Loop Start");
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
        yield return new WaitForSeconds(0.3f);
        m_child.gameObject.SetActive(false);
    }

    public void Reset()
    {
        m_currentTimeLook = 0;
    }
}