using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnigmeHorloge : Singleton<EnigmeHorloge>, IEnigme
{
    [SerializeField, Tooltip("Checkpoint de l'énigme")]
    private Checkpoint m_checkpoint;
    [SerializeField, Tooltip("Porte de l'énigme")]
    private Door m_myDoor;
    private bool m_isCompleted = false;

    public Checkpoint Checkpoint { get; }

    private void Start()
    {
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A)) RestartEnigme();
    }

    public void StartEnigme()
    {
        Debug.Log("Enigme Horloge Start");
        // Fermeture de la porte
        m_myDoor.Close();
    }

    public void RestartEnigme()
    {
        if (m_isCompleted && m_checkpoint.m_finish) return;
        Debug.Log("Enigme Horloge Restart");
        // On désactive le checkpoint
        m_checkpoint.gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    public void CompleteEnigme()
    {
        Debug.Log("Enigme Horloge Complete");
        m_isCompleted = true;
        // Ouverture de la porte
        m_myDoor.OpenLeft();
    }
    protected override string GetSingletonName()
    {
        return "EnigmeHorloge";
    }
}
