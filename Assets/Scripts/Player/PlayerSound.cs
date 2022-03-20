using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private CharacterController m_characterController;
    private void Awake()
    {
        m_characterController = GetComponent<CharacterController>();
    }

    public void UpdateSound()
    {
        Vector3 move = m_characterController.velocity;
        if (move != Vector3.zero)
        {
            if (!SoundManager.Instance.m_PlayerWalkTop.isPlaying)
                SoundManager.Instance.Play(SoundManager.Instance.m_PlayerWalkTop.sound);
            return;
        }

        if (SoundManager.Instance.m_PlayerWalkTop.isPlaying) return;
        SoundManager.Instance.Stop(SoundManager.Instance.m_PlayerWalkTop.sound);
    }
}
