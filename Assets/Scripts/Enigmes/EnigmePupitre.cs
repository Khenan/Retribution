using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnigmePupitre : Singleton<EnigmePupitre>
{
    private int m_lastNum = 0;
    [SerializeField, Tooltip("Numéro Vainqueur du pupitre"), Range(2, 10)]
    public int m_goalNum = 4;
    public delegate void MyDelegate();
    public MyDelegate m_close;
    
    public bool CheckPupitre(int p_numPupitre)
    {
        if (m_lastNum + 1 == p_numPupitre)
        {
            m_lastNum = p_numPupitre;
            print(m_lastNum);
            if (m_lastNum == m_goalNum)
            {
                print("GAGNÉ !!!!");
            }
            return true;
        }
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
