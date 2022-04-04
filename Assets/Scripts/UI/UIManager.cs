using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField, Tooltip("Image du blackFade")]
    private Image m_blackFade;

    [SerializeField, Tooltip("Transition BlackFade")]
    private float m_timeBlackFade = 0.1f;
    [SerializeField, Tooltip("Step alpha BlackFade")]
    private float m_stepAlphaBlackFade = 0.1f;

    private WaitForSeconds m_waitBlackFade;

    private Coroutine m_fadeInCoroutine = null;
    private Coroutine m_fadeOutCoroutine = null;

    private void OnEnable()
    {
        m_waitBlackFade = new WaitForSeconds(m_timeBlackFade);
        m_blackFade.color = new Color(0, 0, 0, 0);
    }

    public void FadeIn()
    {
        if(m_fadeInCoroutine != null) StopCoroutine(m_fadeInCoroutine);
        m_fadeInCoroutine = StartCoroutine(FadeInCoroutine());
    }
    public void FadeOut()
    {
        if(m_fadeOutCoroutine != null) StopCoroutine(m_fadeOutCoroutine);
        m_fadeOutCoroutine = StartCoroutine(FadeOutCoroutine());
    }

    IEnumerator FadeInCoroutine()
    {
        yield return m_waitBlackFade;
        m_blackFade.color = new Color(0, 0, 0, m_blackFade.color.a + m_stepAlphaBlackFade);
        if (m_blackFade.color.a < 1)
        {
            m_fadeInCoroutine = StartCoroutine(FadeInCoroutine());
        }
    }
    IEnumerator FadeOutCoroutine()
    {
        yield return m_waitBlackFade;
        m_blackFade.color = new Color(0, 0, 0, m_blackFade.color.a - m_stepAlphaBlackFade);
        if (m_blackFade.color.a > 0)
        {
            m_fadeOutCoroutine = StartCoroutine(FadeOutCoroutine());
        }
    }
    
    protected override string GetSingletonName()
    {
        return "UIManager";
    }
}
