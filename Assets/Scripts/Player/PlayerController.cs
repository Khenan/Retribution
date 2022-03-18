using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraController)), RequireComponent(typeof(CharaController)), RequireComponent(typeof(InteractionController))]
public class PlayerController : MonoBehaviour
{
    private CameraController m_cameraController;
    private CharaController m_charaController;
    private InteractionController m_interactionController;
    private void Awake()
    {
        m_cameraController = GetComponent<CameraController>();
        m_charaController = GetComponent<CharaController>();
        m_interactionController = GetComponent<InteractionController>();
    }
    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.C))
        {        
            m_charaController.Crouch();
            m_cameraController.CrouchStand(m_charaController.m_isCrouching);
        }
        m_charaController.UpdateMove();
        m_cameraController.UpdateCamera();
        m_interactionController.UpdateInteraction();
    }
}
