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
        if ((x != 0 || z != 0) && !GameManager.Instance.m_inGameMenu)
        {
            if (m_walkStart) return;
            SoundManager.Instance.Play(SoundManager.Instance.m_playerWalkBottom);
            SoundManager.Instance.m_playerWalkBottom.m_event.SetParameter("Progression", 0);
            m_walkStart = true;
            return;
        }

        if (!m_walkStart) return;
        SoundManager.Instance.m_playerWalkBottom.m_event.SetParameter("Progression", 1);
        m_walkStart = false;
    }
}
