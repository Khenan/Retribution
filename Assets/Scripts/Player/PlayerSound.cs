using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public void UpdateSound()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        if (x != 0 || z != 0)
        {
            SoundManager.Instance.Play(SoundManager.Instance.m_PlayerWalkTop);
            return;
        }

        SoundManager.Instance.Stop(SoundManager.Instance.m_PlayerWalkTop);
    }
}
