using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EnigmePupitre : Singleton<EnigmePupitre>, IEnigme
{
    private int m_lastNum = 0;
    [SerializeField, Tooltip("Numéro Vainqueur du pupitre"), Range(2, 10)]
    public int m_goalNum = 4;
    public delegate void MyDelegate();
    public MyDelegate m_close;

    [SerializeField, Tooltip("Le checkpoint de l'énigme")]
    private Checkpoint m_checkpoint;
    public Checkpoint Checkpoint => m_checkpoint;
    
    [SerializeField, Tooltip("Le déclencheur de l'énigme")]
    private GameObject m_triggerStart;

    private bool m_isCompleted = false;
    
    [SerializeField, Tooltip("Le prefab des pupitres")]
    private GameObject m_pupitresPrefab;

    [SerializeField, Tooltip("Fumée de l'énigme")]
    private SmokeBehaviour m_smoke;

    private void Start()
    {
        Instantiate(m_pupitresPrefab, transform);
    }

    public void StartEnigme()
    {
        Debug.Log("L'énigme des pupitres commence !!!");
        
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

    public void RestartEnigme()
    {
        if (m_isCompleted && m_checkpoint.m_finish) return;

        Destroy(transform.GetChild(1).gameObject);
        m_lastNum = 0;
        m_isCompleted = false;
        m_triggerStart.SetActive(true);
        Instantiate(m_pupitresPrefab, transform);
        
        if (!m_smoke) return;
        m_smoke.Restart();
    }

    public void CompleteEnigme()
    {
        m_isCompleted = true;
        m_checkpoint.m_repop = true;
        m_checkpoint.gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    public bool CheckPupitre(int p_numPupitre)
    {
        // Interaction avec le prochain bon pupitre
        if (m_lastNum + 1 == p_numPupitre)
        {
            m_lastNum = p_numPupitre;
            
            // Interaction avec le dernier bon pupitre
            if (m_lastNum == m_goalNum)
            {
                print("GAGNÉ !!!!");
                CompleteEnigme();
            }
            return true;
        }
        // Interaction avec un mauvais pupitre
        m_lastNum = 0;
        m_close?.Invoke();
        print(m_lastNum);
        return false;
    }

    protected override string GetSingletonName()
    {
        return "EnigmePupitre";
    }
}
