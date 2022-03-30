using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraController)), RequireComponent(typeof(CharaController)), RequireComponent(typeof(InteractionController))]
public class PlayerController : MonoBehaviour
{
    private CameraController m_cameraController;
    private CharaController m_charaController;
    private InteractionController m_interactionController;
    private Suffox m_suffox;

    private Coroutine coroutineRespawn = null;
    private Coroutine coroutineDeath = null;

    public Checkpoint m_lastCheckpoint;

    private bool m_isDead = false;
    private void Awake()
    {
        m_cameraController = GetComponent<CameraController>();
        m_charaController = GetComponent<CharaController>();
        m_interactionController = GetComponent<InteractionController>();
        m_suffox = GetComponent<Suffox>();
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

    public void Death()
    {
        m_isDead = true;
        Debug.Log("Le joueur est mort");
        // On stop la coroutine si elle existe
        if(coroutineRespawn != null) StopCoroutine(coroutineRespawn);
        coroutineRespawn = StartCoroutine(RespawnCooldownCoroutine());
    }
    
    private void Respawn()
    {
        m_suffox.RecoverAllOxygen();
        GetComponent<CharacterController>().enabled = false;
        if (m_lastCheckpoint)
        {
            transform.position = m_lastCheckpoint.transform.position;
        }
        else
        {
            transform.position = Vector3.zero;
        }
        GetComponent<CharacterController>().enabled = true;
        EnigmePupitre.Instance.RestartEnigme();
        coroutineDeath = StartCoroutine(ResetDeathCoroutine(1));
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
        Debug.Log("Le joueur peut jouer");
        m_isDead = false;
    }
}
