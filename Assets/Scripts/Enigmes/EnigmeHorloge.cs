using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnigmeHorloge : Singleton<EnigmeHorloge>, IEnigme
{
    [SerializeField, Tooltip("Enfant du couloir")]
    private Transform m_corridorChild;
    [SerializeField, Tooltip("Trigger de l'enfant du couloir")]
    private Transform m_triggerEventCorridorChild;

    private void Awake()
    {
        if(!m_corridorChild) Debug.LogWarning("L'enfant du couloir n'est pas enregistré dans l'énigme", this);
        if(!m_triggerEventCorridorChild) Debug.LogWarning("Le trigger de l'enfant qui cours n'est pas enregistré dans l'énigme", this);
    }

    public Checkpoint Checkpoint { get; }
    public void StartEnigme()
    {
        Debug.Log("Enigme Horloge Start");
    }

    public void RestartEnigme()
    {
        Debug.Log("Enigme Horloge Restart");
    }

    public void CompleteEnigme()
    {
        Debug.Log("Enigme Horloge Complete");
    }
    protected override string GetSingletonName()
    {
        return "EnigmeHorloge";
    }
}
