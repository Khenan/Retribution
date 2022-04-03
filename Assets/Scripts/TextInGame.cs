using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshPro))]
public class TextInGame : MonoBehaviour
{
    private TextMeshPro m_myTMPro;

    [SerializeField, Tooltip("ScripatbleObject Text à contenir")]
    private Text m_myText;
    
    [SerializeField, Tooltip("Le texte doit-il appraitre directement ?")]
    private bool m_displayOnStart = true;
    
    [Header("Si le texte doit apparaitre")]
    [SerializeField, Tooltip("Delay en seconds avant l'apparition du texte"), Range(0f, 5f)]
    private float m_timeToWaitDelay = 0;

    private WaitForSeconds m_waitDelay;
    
    [SerializeField, Tooltip("Temps en seconds pour l'apparition du texte"), Range(0.05f, 1f)]
    private float m_timeToFadeIn = 0.1f;
    
    private WaitForSeconds m_waitFadeIn;
    
    [SerializeField, Tooltip("Rugueur de transition pour l'apparition"), Range(0.001f, 0.1f)]
    private float m_roughness = 0.1f;
    
    [Header("Events")]
    [SerializeField, Tooltip("Events à écouter")]
    private List<Event> m_eventsToListen = new List<Event>();

    private Coroutine m_coroutine = null;

    private void OnEnable()
    {
        GameManager.Instance.m_delegateLanguage += Rewrite;
        if (m_eventsToListen.Count == 0) return;
        foreach (Event e in m_eventsToListen)
        {
            e.m_event += FadeIn;
        }
    }

    private void OnDisable()
    {
        GameManager.Instance.m_delegateLanguage -= Rewrite;
        if (m_eventsToListen.Count == 0) return;
        foreach (Event e in m_eventsToListen)
        {
            e.m_event -= FadeIn;
        }
    }

    private void Awake()
    {
        m_myTMPro = GetComponent<TextMeshPro>();
        m_myTMPro.color = new Color(m_myTMPro.color.b, m_myTMPro.color.g, m_myTMPro.color.b, 0);

        m_waitDelay = new WaitForSeconds(m_timeToWaitDelay);
        m_waitFadeIn = new WaitForSeconds(m_timeToFadeIn);
    }

    private void Start()
    {
        int language = (int)GameManager.Instance.LanguageSelected;
        m_myTMPro.text = m_myText.m_Sentences[language];
        if (!m_displayOnStart) return;
        m_myTMPro.color = new Color(m_myTMPro.color.b, m_myTMPro.color.g, m_myTMPro.color.b, 1);
    }

    private void Rewrite()
    {
        int language = (int)GameManager.Instance.LanguageSelected;
        m_myTMPro.text = m_myText.m_Sentences[language];
    }

    private void FadeIn()
    {
        if(m_coroutine != null) StopCoroutine(m_coroutine);
        m_coroutine = StartCoroutine(DelayCoroutine());
    }

    IEnumerator DelayCoroutine()
    {
        yield return m_waitDelay;
        m_coroutine = StartCoroutine(FadeInCoroutine());
    }
    
    IEnumerator FadeInCoroutine()
    {
        yield return m_waitFadeIn;
        m_myTMPro.color = new Color(m_myTMPro.color.b, m_myTMPro.color.g, m_myTMPro.color.b, m_myTMPro.color.a + m_roughness);
        if (m_myTMPro.color.a < 1)
        {
            m_coroutine = StartCoroutine(FadeInCoroutine());
        }
    }
}
