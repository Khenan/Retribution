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
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        
        // Ouverture du Menu en jeu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.Instance.OpenMenuInGame();
        }
    }

    public void LockCursor(bool p_lock = true)
    {
        Cursor.lockState = p_lock ? CursorLockMode.Confined : CursorLockMode.Locked;
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
    
    

    protected override string GetSingletonName()
    {
        return "GameManager";
    }

}
