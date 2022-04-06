using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private bool m_walkStart = false;
    public void UpdateSound()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        if (x != 0 || z != 0)
        {
            Debug.Log("is moving");
            if (m_walkStart) return;
            Debug.Log("Lance le son");
            SoundManager.Instance.Play(SoundManager.Instance.m_playerWalkBottom);
            SoundManager.Instance.m_playerWalkBottom.m_event.SetParameter("Progression", 0);
            m_walkStart = true;
            return;
        }

        if (!m_walkStart) return;
        Debug.Log("Arrets le son");
        SoundManager.Instance.m_playerWalkBottom.m_event.SetParameter("Progression", 1);
        m_walkStart = false;
    }
}
