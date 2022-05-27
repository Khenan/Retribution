using UnityEngine;

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
        foreach (int value in m_totemsBroken)
        {
            if (value == 0)  return;
        }

        WinGame();
    }

    private void WinGame()
    {
        Debug.Log("La partie est gagnée");
    }

    protected override string GetSingletonName()
    {
        return "GameManager";
    }

}
