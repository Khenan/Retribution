using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnigmeHorloge : Singleton<EnigmeHorloge>, IEnigme
{
    [SerializeField, Tooltip("Checkpoint de l'énigme")] private Checkpoint m_checkpoint;
    [SerializeField, Tooltip("Porte de l'énigme")] private Door m_myDoor;
    [SerializeField, Tooltip("Aiguille des minutes")] private GameObject m_aiguilleMinute;
    [SerializeField, Tooltip("Event qui lance l'énigme")] private Event m_eventStart;
    private bool m_isCompleted = false;
    private Animator m_aiguilleAnimator;
    private int m_aiguilleAnimator_four = Animator.StringToHash("four");
    private int m_aiguilleAnimator_nine = Animator.StringToHash("nine");
    private int m_aiguilleAnimator_zeroEnd = Animator.StringToHash("zeroEnd");
    private int m_aiguilleAnimator_restart = Animator.StringToHash("restart");

    [SerializeField, Tooltip("Event qui place l'aiguille sur le 9")] private Event m_eventNine;
    [SerializeField, Tooltip("Event qui place l'aiguille sur le 0")] private Event m_eventZeroEnd;


    public Checkpoint Checkpoint { get; }

    private void OnEnable()
    {
        m_aiguilleAnimator = m_aiguilleMinute.GetComponent<Animator>();
        m_eventStart.m_event += StartEnigme;
        m_eventNine.m_event += MidEnigme;
        m_eventZeroEnd.m_event += CompleteEnigme;
    }
    
    private void OnDisable()
    {
        m_eventStart.m_event -= StartEnigme;
        m_eventNine.m_event -= MidEnigme;
        m_eventZeroEnd.m_event -= CompleteEnigme;
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
        m_aiguilleAnimator.SetTrigger(m_aiguilleAnimator_four);
    }

    public void RestartEnigme()
    {
        if (m_isCompleted && m_checkpoint.m_finish) return;
        Debug.Log("Enigme Horloge Restart");
        // On désactive le checkpoint
        m_checkpoint.gameObject.GetComponent<BoxCollider>().enabled = false;
        // On remet l'aiguille à zero
        m_aiguilleAnimator.SetTrigger(m_aiguilleAnimator_restart);
    }

    private void MidEnigme()
    {
        m_aiguilleAnimator.SetTrigger(m_aiguilleAnimator_nine);
    }

    public void CompleteEnigme()
    {
        Debug.Log("Enigme Horloge Complete");
        m_isCompleted = true;
        // Ouverture de la porte
        m_myDoor.OpenLeft();
        m_aiguilleAnimator.SetTrigger(m_aiguilleAnimator_zeroEnd);
    }
    protected override string GetSingletonName()
    {
        return "EnigmeHorloge";
    }
}
