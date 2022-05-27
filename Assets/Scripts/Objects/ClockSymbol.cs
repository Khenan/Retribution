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
    
    [SerializeField, Tooltip("Est ce que le symbole est bloqué au début de l'énigme ?")]
    private bool m_isLockOnStart = false;
    private bool m_isLock = false;

    private WaitForSeconds m_cooldownWait = new WaitForSeconds(0.2f);
    private WaitForSeconds m_resetWait = new WaitForSeconds(0.01f);

    private Coroutine m_cooldownCoroutine = null;
    private Coroutine m_loopResetCoroutine = null;

    [SerializeField, Tooltip("Evenement à appeler")]
    private Event m_callEvent;
    [SerializeField, Tooltip("Evenement à écouter")]
    private Event m_listenEvent;
    [SerializeField, Tooltip("Evenement à écouter pour restart")]
    private Event m_listenEventRestart;

    [SerializeField, Tooltip("Son du finish")]
    private SoundEvent m_finalSound;

    private void OnEnable()
    {
        m_listenEventRestart.m_event += Reset;
        if (m_listenEvent == null) return;
        m_listenEvent.m_event += Handle;
    }
    
    private void OnDisable()
    {
        m_listenEventRestart.m_event -= Reset;
        if (m_listenEvent == null) return;
        m_listenEvent.m_event -= Handle;
    }

    private void Awake()
    {
        m_isLock = m_isLockOnStart;
        UpdateAlpha(0);
    }

    public void LookContinue(float p_timeValue)
    {
        if (m_isLock) return;
        if (!GameManager.Instance.m_playerCtrl.gameObject.GetComponent<CharaController>().m_isCrouching) return;
        Debug.Log("Is look !");
        m_currentTimeLook += p_timeValue;
        if (m_currentTimeLook >= m_timeLookTarget)
        {
            Debug.Log("Symbole chargé au max !");
            m_finalSound.Play();
            // Appel de l'event
            if (m_callEvent != null)
            {
                m_callEvent.Raise();
            }
            m_isLock = true;
            GetComponent<Animator>().SetTrigger("pop");
            m_currentTimeLook = m_timeLookTarget;
        }
        UpdateAlpha(m_currentTimeLook / 3);
        
        if (m_isLock) return;
        if(m_cooldownCoroutine != null) StopCoroutine(m_cooldownCoroutine);
        if(m_loopResetCoroutine != null) StopCoroutine(m_loopResetCoroutine);
        m_cooldownCoroutine = StartCoroutine(CooldownResetTimeCoroutine());
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
        UpdateAlpha(m_currentTimeLook / 3);
        if (m_currentTimeLook > 0) m_loopResetCoroutine = StartCoroutine(LoopResetTimeCoroutine());
    }

    private void UpdateAlpha(float p_value)
    {
        Color clr = m_SpriteRenderer.color;
        m_SpriteRenderer.color = new Color(clr.r, clr.g, clr.b, m_currentTimeLook / 3);
    }

    public void Reset()
    {
        m_currentTimeLook = 0;
        m_isLock = m_isLockOnStart;
        UpdateAlpha(0);
    }

    private void Handle()
    {
        m_isLock = false;
    }
}