using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pupitre : InteractibleObject
{
    public bool m_isOpen = false;
    [SerializeField, Tooltip("Animator du Mesh")]
    private Animator m_animator;

    private readonly int m_closeAnimator = Animator.StringToHash("close");
    private readonly int m_openAnimator = Animator.StringToHash("open");

    private EnigmePupitre m_enigmePupitre;
    [SerializeField, Tooltip("Numéro du pupitre"), Range(0, 10)]
    private int m_numPupitre = 0;
    
    [Header("DESK'S SOUNDS")]
    [SerializeField, Tooltip("Son d'ouverture du pupitre")]
    public SoundEvent m_openDesk;
    [SerializeField, Tooltip("Son de fermeture du pupitre")]
    public SoundEvent m_closeDesk;
    
    private void Awake()
    {
        m_enigmePupitre = FindObjectOfType<EnigmePupitre>();
    }

    private void OnEnable()
    {
        EnigmePupitre.Instance.m_close += Close;
    }
    private void OnDisable()
    {
        EnigmePupitre.Instance.m_close -= Close;
    }

    public override void Interact()
    {
        if (m_isOpen) return;
        if (m_enigmePupitre.CheckPupitre(m_numPupitre))
        {
            Open();
        }
    }

    public override void Shine()
    {
        if (m_isOpen) return;
        base.Shine();
    }
    
    private void Open()
    {
        m_isOpen = true;
        print("Open !");
        m_animator.SetTrigger(m_openAnimator);
        StartOpenSound();
    }
    private void Close()
    {
        if (!m_isOpen) return;
        m_isOpen = false;
        print("Close !");
        m_animator.SetTrigger(m_closeAnimator);
        StartCloseSound();
    }
    private void StartOpenSound()
    {
        m_openDesk.m_event.Play();
    }

    private void StartCloseSound()
    {
        float second = Random.Range(0, 0.5f);
        StartCoroutine(CloseSoundCoroutine(second));
    }

    IEnumerator CloseSoundCoroutine(float p_second)
    {
        yield return new WaitForSeconds(p_second);
        m_closeDesk.m_event.Play();
    }
}
