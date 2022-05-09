public interface IEnigme
{
    public Checkpoint Checkpoint { get; }
    public void StartEnigme();
    public void RestartEnigme();
    public void CompleteEnigme();
}
