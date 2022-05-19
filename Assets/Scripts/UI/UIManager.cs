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

    [SerializeField, Tooltip("Texte de la langue séléctionnée")]
    private TextInUI m_textUILanguage;
    [SerializeField, Tooltip("List des textes de langues")]
    private List<Text> m_textsLanguages = new List<Text>();

    private FMOD.Studio.VCA VcaMasterController;
    private FMOD.Studio.VCA VcaSFXController;
    private FMOD.Studio.VCA VcaMusicController;

    private void Start()
    {
        VcaMasterController = FMODUnity.RuntimeManager.GetVCA("vca:/Master");
        VcaSFXController = FMODUnity.RuntimeManager.GetVCA("vca:/SFX");
        VcaMusicController = FMODUnity.RuntimeManager.GetVCA("vca:/Music");

        SetVolumeMaster(3.5f);
        SetVolumeMusic(10f);
        SetVolumeSFX(10f);
    }

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
        GameManager.Instance.m_playerCtrl.m_cameraController.m_mouseSensitivityX = m_sliderX.value;
        GameManager.Instance.m_playerCtrl.m_cameraController.m_mouseSensitivityY = m_sliderY.value;
    }

    public void SetVolumeMaster(float p_volume)
    {
        VcaMasterController.setVolume(p_volume);
    }
    public void SetVolumeSFX(float p_volume)
    {
        VcaSFXController.setVolume(p_volume);
    }
    public void SetVolumeMusic(float p_volume)
    {
        VcaMusicController.setVolume(p_volume);
    }

    public void OpenMenuInGame()
    {
        m_menuSettings.gameObject.SetActive(false);
        
        // Toggle le menu
        bool isOpen = m_menuInGame.gameObject.activeSelf;
        m_menuInGame.gameObject.SetActive(!isOpen);
        GameManager.Instance.m_inGameMenu = !isOpen;
        GameManager.Instance.LockCursor(isOpen);
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

    public void ReloadLastCheckpoint()
    {
        GameManager.Instance.m_playerCtrl.Death();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ChangeLanguage(int p_int)
    {
        GameManager.Instance.ChangeLanguage(p_int);
        m_textUILanguage.m_myText = m_textsLanguages[(int) GameManager.Instance.m_languageSelected];
        m_textUILanguage.Rewrite();
    }

    public void CloseUIInGame()
    {
        m_menuSettings.gameObject.SetActive(false);
        m_menuInGame.gameObject.SetActive(false);
        GameManager.Instance.m_inGameMenu = false;
        GameManager.Instance.LockCursor();
    }
    
    protected override string GetSingletonName()
    {
        return "UIManager";
    }
}
