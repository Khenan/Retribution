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
    private TextMeshPro m_TMPRO;
    [SerializeField, Tooltip("Temps entre chaque lettre")]
    private float m_letterInterval;
    private WaitForSeconds m_wait = null;

    [SerializeField, Tooltip("Phrases du tableau à craie")]
    private List<Text> m_chalkboardSentences = new List<Text>();
    private int m_idSentences = 0;
    private string m_currentSentence = "";
    private Coroutine m_readingCoroutine = null;

    private void Awake()
    {
        if (m_symbolesVaudou.Length < 5)
        {
            Debug.LogWarning("Les 5 symboles vaudou ne sont pas dans le tableau");
        }

        m_TMPRO.text = "";
        m_wait = new WaitForSeconds(m_letterInterval);
    }

    public void Reset()
    {
        m_TMPRO.text = "";
        m_idSentences = 0;
        foreach (GameObject go in m_symbolesVaudou)
        {
            go.SetActive(false);
        }
    }

    public void ReadNextSentence()
    {
        if (m_idSentences >= m_chalkboardSentences.Count) return;
        
        m_symbolesVaudou[m_idSentences].SetActive(true);
        
        m_TMPRO.text = "";
        
        // Special condition
        if (m_idSentences == 4)
        {
            m_symbolesVaudou[0].SetActive(false);
            m_symbolesVaudou[1].SetActive(false);
            m_symbolesVaudou[2].SetActive(false);
            m_symbolesVaudou[3].SetActive(false);
        }
        
        int language = (int) GameManager.Instance.LanguageSelected;
        m_currentSentence = m_chalkboardSentences[m_idSentences].m_Sentences[language];
        
        m_idSentences++;
        
        if(m_readingCoroutine != null)
            StopCoroutine(m_readingCoroutine);
        m_readingCoroutine = StartCoroutine(ReadingCoroutine());
    }

    IEnumerator ReadingCoroutine(int p_id = 0)
    {
        yield return m_wait;
        Debug.Log($"{p_id} / {m_currentSentence.Length}");
        if (p_id < m_currentSentence.Length)
        {
            m_TMPRO.text = $"{m_TMPRO.text}{m_currentSentence[p_id]}";
            m_readingCoroutine = StartCoroutine(ReadingCoroutine(p_id + 1));
        }
    }
}
