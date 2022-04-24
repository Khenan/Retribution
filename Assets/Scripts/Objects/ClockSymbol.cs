using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockSymbol : MonoBehaviour
{
    [SerializeField, Tooltip("Sprite du dessin")]
    private SpriteRenderer m_SpriteRenderer;

    private float m_timeLookTarget = 3f;
    private float m_currentTimeLook = 0f;
    private float m_stepDecreaseTime = 0.01f;

    private WaitForSeconds m_cooldownWait = new WaitForSeconds(0.2f);
    private WaitForSeconds m_resetWait = new WaitForSeconds(0.01f);

    private Coroutine m_cooldownCoroutine = null;
    private Coroutine m_loopResetCoroutine = null;

    private void Awake()
    {
        Color clr = m_SpriteRenderer.color;
        m_SpriteRenderer.color = new Color(clr.r, clr.g, clr.b, 0);
    }

    public void LookContinue(float p_timeValue)
    {
        Debug.Log("Is look !");
        m_currentTimeLook += p_timeValue;
        if (m_currentTimeLook > m_timeLookTarget) m_currentTimeLook = m_timeLookTarget;
        UpdateAlpha();
        if(m_cooldownCoroutine != null) StopCoroutine(m_cooldownCoroutine);
        if(m_loopResetCoroutine != null) StopCoroutine(m_loopResetCoroutine);
        m_cooldownCoroutine = StartCoroutine(CooldownResetTimeCoroutine());
    }

    IEnumerator CooldownResetTimeCoroutine()
    {
        yield return m_cooldownWait;
        Debug.Log("Reset Loop Start");
        m_loopResetCoroutine = StartCoroutine(LoopResetTimeCoroutine());
    }
    IEnumerator LoopResetTimeCoroutine()
    {
        yield return m_resetWait;
        m_currentTimeLook -= m_stepDecreaseTime;
        if (m_currentTimeLook < 0) m_currentTimeLook = 0;
        UpdateAlpha();
        if (m_currentTimeLook > 0) m_loopResetCoroutine = StartCoroutine(LoopResetTimeCoroutine());
    }

    private void UpdateAlpha()
    {
        Color clr = m_SpriteRenderer.color;
        m_SpriteRenderer.color = new Color(clr.r, clr.g, clr.b, m_currentTimeLook / 3);
    }
}
