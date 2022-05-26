using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private bool m_walkStart = false;
    private bool m_isWalking = false;
    private Coroutine m_coroutine;
    
    public void UpdateSound()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        if ((x != 0 || z != 0) && m_coroutine == null && !m_isWalking)
        {
            m_coroutine = StartCoroutine(IsWalkCoroutine());
        }
        UpdateWalking();
    }

    public void Dead()
    {
        SoundManager.Instance.Play(SoundManager.Instance.m_playerDeathSound);
    }

    IEnumerator IsWalkCoroutine()
    {
        Debug.Log("Lancement Coroutine Walk");
        yield return new WaitForSeconds(0.3f);
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        m_isWalking = x != 0 || z != 0;
        m_coroutine = null;
    }

    private void UpdateWalking()
    {
        if (m_isWalking)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            if (x != 0 || z != 0)
            {
                if (m_walkStart) return;
                SoundManager.Instance.Play(SoundManager.Instance.m_PlayerWalkBottom);
                SoundManager.Instance.m_PlayerWalkBottom.m_event.SetParameter("Progression", 0);
                m_walkStart = true;
                return;
            }
        }
        if (!m_walkStart) return;
        SoundManager.Instance.m_PlayerWalkBottom.m_event.SetParameter("Progression", 1);
        m_walkStart = false;
        m_isWalking = false;
    }
}
