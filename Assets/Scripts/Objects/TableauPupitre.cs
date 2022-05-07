using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TableauPupitre : MonoBehaviour
{
    [Header("Symboles Vaudou")]
    [SerializeField, Tooltip("Symbole Vaudou 1")]
    private GameObject[] m_symbolesVaudou = new GameObject[5];

    [Header("Lecture texte")]
    [SerializeField, Tooltip("Texte des phrases du tableau à craie")]
    private TextMeshPro m_myTMPro;
    [SerializeField, Tooltip("Temps entre chaque lettre")]
    private float m_letterInterval;
    private WaitForSeconds m_wait = null;

    [SerializeField, Tooltip("Phrases du tableau à craie")]
    private List<Text> m_chalkboardSentences = new List<Text>();
    public int m_idSentences = -1;
    private string m_currentSentence = "";
    private Coroutine m_readingCoroutine = null;

    private bool m_writing = false;

    private void OnEnable()
    {
        GameManager.Instance.m_delegateLanguage += Rewrite;
    }

    private void OnDisable()
    {
        GameManager.Instance.m_delegateLanguage -= Rewrite;
    }

    private void Awake()
    {
        if (m_symbolesVaudou.Length < 5)
        {
            Debug.LogWarning("Les 5 symboles vaudou ne sont pas dans le tableau");
        }
        else
        {
            foreach (GameObject go in m_symbolesVaudou)
            {
                go.SetActive(false);
            }
        }

        m_myTMPro.text = "";
        m_wait = new WaitForSeconds(m_letterInterval);
    }

    public void Reset()
    {
        m_myTMPro.text = "";
        m_idSentences = -1;
        foreach (GameObject go in m_symbolesVaudou)
        {
            go.SetActive(false);
        }
    }

    public void ReadNextSentence()
    {
        if (m_idSentences >= m_chalkboardSentences.Count) return;
        m_idSentences++;
        m_symbolesVaudou[m_idSentences].SetActive(true);
        
        m_myTMPro.text = "";
        
        // Special condition
        if (m_idSentences == 4)
        {
            m_symbolesVaudou[0].SetActive(false);
            m_symbolesVaudou[1].SetActive(false);
            m_symbolesVaudou[2].SetActive(false);
            m_symbolesVaudou[3].SetActive(false);
        }
        
        int language = (int) GameManager.Instance.m_languageSelected;
        m_currentSentence = m_chalkboardSentences[m_idSentences].m_Sentences[language];
        
        if(m_readingCoroutine != null)
            StopCoroutine(m_readingCoroutine);
        m_readingCoroutine = StartCoroutine(ReadingCoroutine());
        StartSound();
    }

    IEnumerator ReadingCoroutine(int p_id = 0)
    {
        yield return m_wait;
        // Debug.Log($"{p_id} / {m_currentSentence.Length}");
        if (p_id < m_currentSentence.Length)
        {
            m_myTMPro.text = $"{m_myTMPro.text}{m_currentSentence[p_id]}";
            m_readingCoroutine = StartCoroutine(ReadingCoroutine(p_id + 1));
        }
        else
        {
            StopSound();
        }
    }

    private void Rewrite()
    {
        if (m_idSentences < 0) return;
        if(m_readingCoroutine != null)
            StopCoroutine(m_readingCoroutine);
        
        int language = (int)GameManager.Instance.m_languageSelected;
        m_myTMPro.text = m_chalkboardSentences[m_idSentences].m_Sentences[language];
    }

    private void StartSound()
    {
        if (m_writing) return;
        SoundManager.Instance.Play(SoundManager.Instance.m_chalkboardWriting);
        //SoundManager.Instance.m_chalkboardWriting.m_event.SetParameter("Progression", 0);
        m_writing = true;
    }

    private void StopSound()
    {
        if (!m_writing) return;
        SoundManager.Instance.Stop(SoundManager.Instance.m_chalkboardWriting);
        //SoundManager.Instance.m_chalkboardWriting.m_event.SetParameter("Progression", 1);
        m_writing = false;
    }
}
