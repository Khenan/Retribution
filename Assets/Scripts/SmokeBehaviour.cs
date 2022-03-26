using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json.Serialization;
using UnityEngine;
using UnityEngine.VFX;

public class SmokeBehaviour : MonoBehaviour
{
    [SerializeField, Tooltip("Plafond de la fumée, placez la fumée à la hauteur maximal qu'elle pourrait aller et inscrivez la valeur dans le champs"), Range(0, 10)]
    private float m_maxPosY = 4;
    [SerializeField, Tooltip("Vitesse de la fumée en Y en m/s"), Range(0, 1)]
    private float m_speedY = 0.01f;
    
    [HideInInspector]
    public Transform m_smoke;

    [HideInInspector]
    public bool m_start = false;
    
    private Vector3 m_initPos = Vector3.zero;

    private float m_currentTime = 0;

    private void Awake()
    {
        m_smoke = transform.GetChild(0);
        m_smoke.GetComponent<VisualEffect>().playRate = 0;
        m_initPos = transform.localPosition;
    }

    private void FixedUpdate()
    {
        if(!m_start) return;
        m_currentTime += Time.deltaTime;
        transform.Translate(0, 1f * m_speedY * Time.deltaTime, 0);
        if (transform.localPosition.y < m_maxPosY) return;
        m_start = false;
        transform.localPosition = new Vector3(transform.localPosition.x, m_maxPosY, transform.localPosition.z);
        Debug.Log("finish ! " + m_currentTime);
    }

    public void Begin()
    {
        m_start = true;
    }

    public void Restart()
    {
        m_start = false;
        m_smoke.GetComponent<VisualEffect>().playRate = 0;
        transform.localPosition = m_initPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        print("Dedans");
    }

    private void OnTriggerExit(Collider other)
    {
        print("Dehors");
    }
}
