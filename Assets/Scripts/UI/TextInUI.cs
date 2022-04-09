using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextInUI : MonoBehaviour
{
    private TextMeshProUGUI m_myTMProUGUI;

    [SerializeField, Tooltip("ScripatbleObject Text à contenir")]
    public Text m_myText;

    private void OnEnable()
    {
        GameManager.Instance.m_delegateLanguage += Rewrite;
        Rewrite();
    }

    private void OnDisable()
    {
        GameManager.Instance.m_delegateLanguage -= Rewrite;
    }

    private void Awake()
    {
        m_myTMProUGUI = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        Rewrite();
    }

    public void Rewrite()
    {
        int language = (int)GameManager.Instance.m_languageSelected;
        m_myTMProUGUI.text = m_myText.m_Sentences[language];
    }
}
