using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public delegate void MyDelegate();

    public MyDelegate m_delegateLanguage;
    
    public enum Languages
    {
        ENGLISH,
        FRENCH
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (LanguageSelected == Languages.ENGLISH)
            {
                ChangeLanguage(Languages.FRENCH);
                return;
            }
            ChangeLanguage(Languages.ENGLISH);
        }
    }

    public Languages LanguageSelected = Languages.ENGLISH;

    protected override string GetSingletonName()
    {
        return "GameManager";
    }

    private void ChangeLanguage(Languages p_language)
    {
        LanguageSelected = p_language;
        m_delegateLanguage?.Invoke();
    }
}
