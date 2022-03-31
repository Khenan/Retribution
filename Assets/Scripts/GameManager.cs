
public class GameManager : Singleton<GameManager>
{
    public enum Languages
    {
        ENGLISH,
        FRENCH
    }

    public Languages LanguageSelected = Languages.ENGLISH;

    protected override string GetSingletonName()
    {
        return "GameManager";
    }
}
