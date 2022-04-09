using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField, Tooltip("Menu in game")]
    public Transform m_menuInGame;
    [SerializeField, Tooltip("Menu Settings")]
    public Transform m_menuSettings;
    [SerializeField, Tooltip("Image du blackFade")]
    private Image m_blackFade;

    [SerializeField, Tooltip("Transition BlackFade")]
    private float m_timeBlackFade = 0.1f;
    [SerializeField, Tooltip("Step alpha BlackFade")]
    private float m_stepAlphaBlackFade = 0.1f;

    private WaitForSeconds m_waitBlackFade;

    private Coroutine m_fadeInCoroutine = null;
    private Coroutine m_fadeOutCoroutine = null;
    
    
    [SerializeField, Tooltip("Slider Sensibilité X")]
    private Slider m_sliderX;
    [SerializeField, Tooltip("Slider Sensibilité Y")]
    private Slider m_sliderY;

    [SerializeField, Tooltip("Joueur")]
    private PlayerController m_playerCtrl;

    private void OnEnable()
    {
        if (m_menuInGame && m_menuSettings)
        {
            m_menuInGame.gameObject.SetActive(false);
            m_menuSettings.gameObject.SetActive(false);
        }
        
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

    public void ChangeSensitivity()
    {
        Debug.Log(m_sliderX.value);
        m_playerCtrl.m_cameraController.m_mouseSensitivityX = m_sliderX.value;
        m_playerCtrl.m_cameraController.m_mouseSensitivityY = m_sliderY.value;
    }

    public void OpenMenuInGame()
    {
        // Si dans menu des paramètres 
        if (m_menuSettings.gameObject.activeSelf)
        {
            m_menuSettings.gameObject.SetActive(false);
        }
        
        // Toggle le menu
        bool isOpen = m_menuInGame.gameObject.activeSelf;
        m_menuInGame.gameObject.SetActive(!isOpen);
        GameManager.Instance.m_inGameMenu = !isOpen;
    }
    public void OpenMenuSettings()
    {
        m_menuInGame.gameObject.SetActive(false);
        m_menuSettings.gameObject.SetActive(true);
    }

    public void ChangeScene(int p_id)
    {
        SceneManager.Instance.ChangeScene(p_id);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    protected override string GetSingletonName()
    {
        return "UIManager";
    }
}
