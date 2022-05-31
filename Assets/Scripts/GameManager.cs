using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GameManager : Singleton<GameManager>
{
    [Tooltip("Si le joueur est dans le menu en jeu")]
    public bool m_inGameMenu = false;
    public delegate void MyDelegate();

    public MyDelegate m_delegateLanguage;

    [SerializeField, Tooltip("Texture du curseur de la souris")]
    private Texture2D m_textureCursor;

    [SerializeField, Tooltip("Joueur")]
    public PlayerController m_playerCtrl;

    public int[] m_totemsBroken = new[] {0, 0, 0};

    public Door m_doorCoridor;
    public List<TriggerBoxEvent> m_triggerBoxes;
    public List<GameObject> m_lockSymbols;

    private int m_nbLanguages = 2;
    public enum Languages
    {
        ENGLISH = 0,
        FRENCH = 1
    }
    [Tooltip("Langue selectionnée au début du jeu")]
    public Languages m_languageSelected = Languages.ENGLISH;

    private void Awake()
    {
        Cursor.SetCursor(m_textureCursor, Vector2.zero, CursorMode.Auto);
    }

    private void Update()
    {
        
        // Ouverture du Menu en jeu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.Instance.OpenMenuInGame();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            //BreakTotem(3);
        }
    }
    /// <summary>
    /// Allow Cursor to be Locked Mode or Confined Mode
    /// </summary>
    /// <param name="value">Activate or deactivate the Locked Mode, where true activates the Locked Mode and false activates the Confined Mode.</param>
    public void LockCursor(bool value = true)
    {
        Cursor.lockState = value ? CursorLockMode.Locked : CursorLockMode.Confined;
    }

    public void ChangeLanguage(int p_int)
    {
        int id = (int) m_languageSelected;
        id += p_int;

        if (id < 0) id = m_nbLanguages - 1;
        if (id >= m_nbLanguages) id = 0;
        
        SetLanguage((Languages)id);
    }
    private void SetLanguage(Languages p_language)
    {
        m_languageSelected = p_language;
        m_delegateLanguage?.Invoke();
    }

    public void BreakTotem(int p_id)
    {
        int id = p_id - 1;
        if (id >= 0 && id < m_totemsBroken.Length)
            m_totemsBroken[id] = 1;

        if (p_id == 3)
        {
            m_lockSymbols.ForEach(e => e.SetActive(false));
            m_doorCoridor.ClearCollider();
            m_doorCoridor.OpenLeft();
            m_triggerBoxes.ForEach(e => e.Pop());
        }
    }

    protected override string GetSingletonName()
    {
        return "GameManager";
    }

}
