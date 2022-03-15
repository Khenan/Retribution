using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField, Tooltip("Sensibilité de la souris sur l'axe X")]
    private float m_mouseSensitivityX = 100f;
    [SerializeField, Tooltip("Sensibilité de la souris sur l'axe Y")]
    private float m_mouseSensitivityY = 100f;
    
    [SerializeField, Tooltip("Vue maximale vers le sol")]
    private float m_minViewY = -90f;
    [SerializeField, Tooltip("Vue maximale vers le ciel")]
    private float m_maxViewY = 90f;

    [SerializeField, Tooltip("Transform de la caméra")]
    private Transform m_cameraTransform;

    private float m_rotationX = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        m_cameraTransform.rotation = Quaternion.identity;
    }

    private void Update()
    {
        float x = Input.GetAxis("Mouse X") * m_mouseSensitivityX * Time.deltaTime;
        float y = Input.GetAxis("Mouse Y") * m_mouseSensitivityY * Time.deltaTime;

        m_rotationX -= y;
        m_rotationX = Mathf.Clamp(m_rotationX, m_minViewY, m_maxViewY);
        
        m_cameraTransform.localRotation = Quaternion.Euler(m_rotationX, 0f, 0f);
        transform.Rotate(Vector3.up * x);
    }
}
