using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraController)), RequireComponent(typeof(CharaController)), RequireComponent(typeof(InteractionController))]
public class PlayerController : MonoBehaviour
{
    private CameraController m_cameraController;
    private CharaController m_charaController;
    private InteractionController m_interactionController;

    public Checkpoint m_lastCheckpoint;

    private bool m_isDead = false;
    private void Awake()
    {
        m_cameraController = GetComponent<CameraController>();
        m_charaController = GetComponent<CharaController>();
        m_interactionController = GetComponent<InteractionController>();
    }
    private void Update()
    {
        
        m_charaController.UpdateGravity();

        if (!m_isDead)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                Death();
                EnigmePupitre.Instance.RestartEnigme();
            }
            if (Input.GetKeyDown(KeyCode.C))
            {        
                m_charaController.Crouch();
                m_cameraController.CrouchStand(m_charaController.m_isCrouching);
            }
            m_charaController.UpdateMove();
            m_interactionController.UpdateInteraction();
            m_cameraController.UpdateCamera();
        }
        
    }

    private void Death()
    {
        m_isDead = true;
        StopCoroutine(RespawnCooldownCoroutine());
        StartCoroutine(RespawnCooldownCoroutine());
    }
    
    private void Respawn()
    {
        GetComponent<CharacterController>().enabled = false;
        transform.position = m_lastCheckpoint.transform.position;
        GetComponent<CharacterController>().enabled = true;
        StartCoroutine(ResetDeathCoroutine(1));
    }
    
    public void SetCheckpoint(Checkpoint p_checkpoint)
    {
        m_lastCheckpoint = p_checkpoint;
        Debug.Log($"Mon checkpoint est {m_lastCheckpoint.name}");
    }

    IEnumerator RespawnCooldownCoroutine(float p_waitSeconds = 2)
    {
        yield return new WaitForSeconds(p_waitSeconds);
        Respawn();
    }
    IEnumerator ResetDeathCoroutine(float p_waitSeconds = 2)
    {
        yield return new WaitForSeconds(p_waitSeconds);
        m_isDead = false;
    }
}
