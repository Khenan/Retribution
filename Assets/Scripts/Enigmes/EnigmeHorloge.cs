using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EnigmeHorloge : Singleton<EnigmeHorloge>, IEnigme
{
    [SerializeField, Tooltip("Checkpoint de l'énigme")] private Checkpoint m_checkpoint;
    [SerializeField, Tooltip("Porte de l'énigme")] private Door m_myDoor;
    [SerializeField, Tooltip("Aiguille des minutes")] private GameObject m_aiguilleMinute;
    [SerializeField, Tooltip("Aiguille Fantome des minutes")] private GameObject m_aiguilleFantome;
    [SerializeField, Tooltip("Event qui lance l'énigme")] private Event m_eventStart;
    [SerializeField, Tooltip("Porte vitrée de l'horloge")] private GameObject m_glassDoor;
    private bool m_isCompleted = false;
    
    private Animator m_aiguilleAnimator;
    private Animator m_aiguilleFantomeAnimator;
    private int m_aiguilleAnimator_four = Animator.StringToHash("four");
    private int m_aiguilleAnimator_nine = Animator.StringToHash("nine");
    private int m_aiguilleAnimator_zeroEnd = Animator.StringToHash("zeroEnd");
    private int m_aiguilleAnimator_restart = Animator.StringToHash("reset");

    [SerializeField, Tooltip("Event qui place l'aiguille sur le 9")] private Event m_eventNine;
    [SerializeField, Tooltip("Event qui place l'aiguille sur le 0")] private Event m_eventZeroEnd;
    [SerializeField, Tooltip("Event qui lance la fumée")] private Event m_eventLaunchSmoke;
    
    private Animator m_glassDoorAnimator;
    private int m_glassDoorAnimator_restart = Animator.StringToHash("restart");
    private int m_glassDoorAnimator_open = Animator.StringToHash("open");
    
    
    [SerializeField, Tooltip("Fumée de l'énigme")]
    private SmokeBehaviour m_smoke;
    
    public Checkpoint Checkpoint { get; }
    
    [SerializeField, Tooltip("Event à appeler pour restart")] private Event m_eventRestart;

    private bool m_enigmeStarted = false;
    
    
    [SerializeField, Tooltip("Texte Indice")]
    private Transform m_onYourKnees;

    private bool m_midEnigme = false;

    private void OnEnable()
    {
        m_onYourKnees.gameObject.SetActive(false);
        m_aiguilleAnimator = m_aiguilleMinute.GetComponent<Animator>();
        m_aiguilleFantomeAnimator = m_aiguilleFantome.GetComponent<Animator>();
        m_glassDoorAnimator = m_glassDoor.GetComponent<Animator>();
        m_eventStart.m_event += StartEnigme;
        m_eventNine.m_event += MidEnigme;
        m_eventZeroEnd.m_event += CompleteEnigme;
        m_eventLaunchSmoke.m_event += LaunchSmoke;
        // Fermeture de la porte
        m_myDoor.Close();
    }
    
    private void OnDisable()
    {
        m_eventStart.m_event -= StartEnigme;
        m_eventNine.m_event -= MidEnigme;
        m_eventZeroEnd.m_event -= CompleteEnigme;
        m_eventLaunchSmoke.m_event -= LaunchSmoke;
    }

    private void LaunchSmoke()
    {
        if (m_smoke)
        {
            m_smoke.m_smoke.GetComponent<VisualEffect>().playRate = 1;
            m_smoke.Begin();
        }
        else
        {
            Debug.LogWarning("Attention, aucune fumée n'a été attribué à l'énigme, elle ne peut donc pas apparaître", this);
        }
    }

    public void StartEnigme()
    {
        Debug.Log("Enigme Horloge Start");
        m_enigmeStarted = true;
        // Fermeture de la porte
        m_myDoor.Close();
        m_myDoor.Lock();
        m_aiguilleAnimator.SetTrigger(m_aiguilleAnimator_four);
        m_aiguilleFantomeAnimator.SetTrigger(m_aiguilleAnimator_four);
        StartCoroutine(DisplayIndiceCoroutine());
    }

    public void RestartEnigme()
    {
        if (m_isCompleted && m_checkpoint.m_finish) return;
        Debug.Log("Enigme Horloge Restart");
        // On désactive le checkpoint
        //m_checkpoint.gameObject.GetComponent<BoxCollider>().enabled = false;
        // On remet l'aiguille à zero
        m_aiguilleAnimator.SetTrigger(m_aiguilleAnimator_restart);
        m_aiguilleFantomeAnimator.SetTrigger(m_aiguilleAnimator_restart);
        m_glassDoorAnimator.SetTrigger(m_glassDoorAnimator_restart);
        // Ouverture de la porte
        if(m_enigmeStarted)
            m_myDoor.OpenLeft();
        
        m_eventRestart.Raise();
        
        m_onYourKnees.gameObject.SetActive(false);
        m_midEnigme = false;

        m_enigmeStarted = false;
        if (!m_smoke) return;
        m_smoke.Restart();
    }

    private void MidEnigme()
    {
        m_midEnigme = true;
        m_aiguilleAnimator.SetTrigger(m_aiguilleAnimator_nine);
        m_aiguilleFantomeAnimator.SetTrigger(m_aiguilleAnimator_nine);
    }

    public void CompleteEnigme()
    {
        Debug.Log("Enigme Horloge Complete");
        m_isCompleted = true;
        // On désactive le checkpoint
        m_checkpoint.gameObject.GetComponent<BoxCollider>().enabled = false;
        // Ouverture de la porte
        m_myDoor.OpenLeft();
        m_aiguilleAnimator.SetTrigger(m_aiguilleAnimator_zeroEnd);
        m_aiguilleFantomeAnimator.SetTrigger(m_aiguilleAnimator_zeroEnd);
        StartCoroutine(OpenGlassDoorCoroutine());
    }

    IEnumerator OpenGlassDoorCoroutine()
    {
        yield return new WaitForSeconds(2.2f);
        m_glassDoorAnimator.SetTrigger(m_glassDoorAnimator_open);
    }
    IEnumerator DisplayIndiceCoroutine()
    {
        yield return new WaitForSeconds(5f);
        if(m_midEnigme) yield break;
        m_onYourKnees.gameObject.SetActive(true);
    }
    protected override string GetSingletonName()
    {
        return "EnigmeHorloge";
    }
}
