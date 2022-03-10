using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharaController : MonoBehaviour
{
    private Rigidbody m_rb;

    [SerializeField, Tooltip("Vitesse du personnage")]
    private float m_speed = 1.6f;

    private Vector3 m_move;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        m_rb.velocity = m_move;
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        m_move = transform.right * x + transform.forward * z;
    }
}
