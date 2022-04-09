using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Tooltip("Si le joueur est dans le menu en jeu")]
    public bool m_inGameMenu = false;
    public delegate void MyDelegate();

    public MyDelegate m_delegateLanguage;

    [SerializeField, Tooltip("Texture du curseur de la souris")]
    private Texture2D m_textureCursor;

    
    public enum Languages
    {
        ENGLISH,
        FRENCH
    }
    [Tooltip("Langue selectionnée au début du jeu")]
    public Languages m_languageSelected = Languages.ENGLISH;

    private void Awake()
    {
        Cursor.SetCursor(m_textureCursor, Vector2.zero, CursorMode.Auto);
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update()
    {
        
        // Ouverture du Menu en jeu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.Instance.OpenMenuInGame();
            
            // Cursor.lockState = m_inGameMenu ? CursorLockMode.Confined : CursorLockMode.Locked;
        }
        // Changer de langue
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (m_languageSelected == Languages.ENGLISH)
            {
                ChangeLanguage(Languages.FRENCH);
                return;
            }
            ChangeLanguage(Languages.ENGLISH);
        }
    }
    private void ChangeLanguage(Languages p_language)
    {
        m_languageSelected = p_language;
        m_delegateLanguage?.Invoke();
    }
    
    

    protected override string GetSingletonName()
    {
        return "GameManager";
    }

}
