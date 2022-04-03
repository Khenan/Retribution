using System;
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
        m_myTMPro = GetComponent<TextMeshPro>();
    }

    private void Start()
    {
        if (!m_displayOnStart) return;
        m_myTMPro.text = m_myText.m_Sentences[(int) GameManager.Instance.LanguageSelected];
    }

    private void Rewrite()
    {
        m_myTMPro.text = m_myText.m_Sentences[(int) GameManager.Instance.LanguageSelected];
    }
}
