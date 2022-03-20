using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnigmePupitre : Singleton<EnigmePupitre>, IEnigme
{
    private int m_lastNum = 0;
    [SerializeField, Tooltip("Numéro Vainqueur du pupitre"), Range(2, 10)]
    public int m_goalNum = 4;
    public delegate void MyDelegate();
    public MyDelegate m_close;

    [SerializeField, Tooltip("Cube Test de déclenchement")]
    private GameObject m_myCubeTrigger;

    public void StartEnigme()
    {
        Debug.Log("L'énigme des pupitres commence !!!");
        m_myCubeTrigger.GetComponent<Rigidbody>().isKinematic = false;
    }
    
    public bool CheckPupitre(int p_numPupitre)
    {
        // Interaction avec le prochain bon pupitre
        if (m_lastNum + 1 == p_numPupitre)
        {
            m_lastNum = p_numPupitre;
            print(m_lastNum);
            
            // Interaction avec le dernier bon pupitre
            if (m_lastNum == m_goalNum)
            {
                print("GAGNÉ !!!!");
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
