using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suffox : MonoBehaviour
{
    private PlayerController m_playerCtrl;

    [SerializeField, Tooltip("Point d'oxygène du joueur"), Range(0, 100)]
    private float m_cooldownTakeDamage = 0.1f;
    [SerializeField, Tooltip("Point d'oxygène du joueur"), Range(0, 100)]
    private float m_cooldownRecoverOxygen = 0.1f;
    
    [SerializeField, Tooltip("Point d'oxygène du joueur"), Range(0, 100)]
    private float m_oxygenMax = 100;

    public float m_oxygen;
    

    private void Awake()
    {
        m_oxygen = m_oxygenMax;
        m_playerCtrl = GetComponent<PlayerController>();
    }

    public void TakeDamage()
    {
        StopAllCoroutines();
        StartCoroutine(TakeDamageCoroutine(m_cooldownTakeDamage));
    }
    public void RecoverOxygen()
    {
        StopAllCoroutines();
        StartCoroutine(RecoverOxygenCoroutine(m_cooldownRecoverOxygen));
    }

    IEnumerator TakeDamageCoroutine(float p_second)
    {
        yield return new WaitForSeconds(p_second);
        if (m_oxygen > 0)
        {
            m_oxygen--;
            if (m_oxygen <= 0)
            {
                m_oxygen = 0;
                if (m_playerCtrl)
                {
                    m_playerCtrl.Death();
                }
            }
            StartCoroutine(TakeDamageCoroutine(p_second));
        }

    }
    IEnumerator RecoverOxygenCoroutine(float p_second)
    {
        yield return new WaitForSeconds(p_second);
        
        if (m_oxygen < m_oxygenMax)
        {
            m_oxygen++;
            if (m_oxygen >= m_oxygenMax)
            {
                m_oxygen = m_oxygenMax;
            }
            StartCoroutine(RecoverOxygenCoroutine(p_second));
        }

    }
}
