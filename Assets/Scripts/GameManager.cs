using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [HideInInspector, Tooltip("Si le joueur est dans le menu en jeu")]
    public bool m_inGameMenu = false;
    public delegate void MyDelegate();

    public MyDelegate m_delegateLanguage;

    
    public enum Languages
    {
        ENGLISH,
        FRENCH
    }
    [Tooltip("Langue selectionnée au début du jeu")]
    public Languages m_languageSelected = Languages.ENGLISH;

    private void Update()
    {
        
        // Ouverture du Menu en jeu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject UIMenu = UIManager.Instance.m_menuInGame.gameObject;
            bool isOpen = UIMenu.activeSelf;
            UIMenu.SetActive(!isOpen);
            m_inGameMenu = !isOpen;
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
