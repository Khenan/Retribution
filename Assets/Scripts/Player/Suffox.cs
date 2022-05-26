using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Suffox : MonoBehaviour
{
    private PlayerController m_playerCtrl;

    [SerializeField, Tooltip("(1 = 1 seconde) En combien de temps le joueur perd 1 oxygène"), Range(0, 100)]
    private float m_cooldownTakeDamage = 0.1f;
    [SerializeField, Tooltip("(1 = 1 seconde) En combien de temps le joueur récupère 1 oxygène"), Range(0, 100)]
    private float m_cooldownRecoverOxygen = 0.1f;
    
    [SerializeField, Tooltip("Point d'oxygène du joueur"), Range(0, 100)]
    private float m_oxygenMax = 100;

    public float m_oxygen;
    
    // UI
    [SerializeField, Tooltip("Slider UI d'oxygène")]
    private Slider m_oxygenSlider;

    [SerializeField, Tooltip("Slider UI d'oxygène")]
    private List<Image> m_oxygenImages = new List<Image>();
    
    // Couch Step
    public bool m_cough_0;
    public bool m_cough_1;
    public bool m_cough_2;
    
    private void Awake()
    {
        m_oxygen = m_oxygenMax;
        m_playerCtrl = GetComponent<PlayerController>();
        UpdateSlider();
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
    public void RecoverAllOxygen()
    {
        StopAllCoroutines();
        m_oxygen = m_oxygenMax;
        UpdateSlider();
        ClearImage();
    }

    IEnumerator TakeDamageCoroutine(float p_second)
    {
        yield return new WaitForSeconds(p_second);
        UpdateSlider();
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
            UpdateSuffoxImageFadeIn();
            StartCoroutine(TakeDamageCoroutine(p_second));
        }
    }
    IEnumerator RecoverOxygenCoroutine(float p_second)
    {
        yield return new WaitForSeconds(p_second);
        UpdateSlider();
        if (!(m_oxygen < m_oxygenMax)) yield break;
        m_oxygen++;
        if (m_oxygen >= m_oxygenMax)
        {
            m_oxygen = m_oxygenMax;
        }
        UpdateSuffoxImageFadeOut();
        StartCoroutine(RecoverOxygenCoroutine(p_second));
    }

    private void UpdateSlider()
    {
        if (!m_oxygenSlider)
        {
            Debug.LogWarning("Le slider d'oxygène n'est pas attribué");
            return;
        }

        m_oxygenSlider.value = m_oxygen;
    }

    private void UpdateSuffoxImageFadeIn()
    {
        ResetAnim();
        if (m_oxygen < 20)
        {
            m_oxygenImages[2].GetComponent<Animator>().SetTrigger("fadeIn");
            if (m_cough_2) return;
            m_cough_2 = true;
            Cough();
        }
        else if (m_oxygen < 50)
        {
            m_oxygenImages[1].GetComponent<Animator>().SetTrigger("fadeIn");  
            if (m_cough_1) return;
            m_cough_1 = true;
            Cough();
        }
        else if (m_oxygen < 100)
        {
            m_oxygenImages[0].GetComponent<Animator>().SetTrigger("fadeIn");  
            if (m_cough_0) return;
            m_cough_0 = true;
            Cough();
        }
    }

    private void Cough()
    {
        SoundManager.Instance.Stop(SoundManager.Instance.m_playerCoughSound);
        SoundManager.Instance.Play(SoundManager.Instance.m_playerCoughSound); 
    }

    private void UpdateSuffoxImageFadeOut()
    {
        ResetAnim();
        if(m_oxygen >= 100)
        {
            m_oxygenImages[0].GetComponent<Animator>().SetTrigger("fadeOut");
            m_cough_0 = false;
        }
        else if(m_oxygen > 50)
        {
            m_oxygenImages[1].GetComponent<Animator>().SetTrigger("fadeOut");
            m_cough_1 = false;
        }
        else if(m_oxygen > 20)
        {
            m_oxygenImages[2].GetComponent<Animator>().SetTrigger("fadeOut");
            m_cough_2 = false;
        }
    }

    private void ResetAnim()
    {
        m_oxygenImages[0].GetComponent<Animator>().ResetTrigger("fadeIn");
        m_oxygenImages[0].GetComponent<Animator>().ResetTrigger("fadeOut");
        m_oxygenImages[1].GetComponent<Animator>().ResetTrigger("fadeIn");
        m_oxygenImages[1].GetComponent<Animator>().ResetTrigger("fadeOut");
        m_oxygenImages[2].GetComponent<Animator>().ResetTrigger("fadeIn");
        m_oxygenImages[2].GetComponent<Animator>().ResetTrigger("fadeOut");
    }

    private void ClearImage()
    {
        ResetAnim();
        m_oxygenImages[0].GetComponent<Animator>().SetTrigger("fadeOut");
        m_oxygenImages[1].GetComponent<Animator>().SetTrigger("fadeOut");
        m_oxygenImages[2].GetComponent<Animator>().SetTrigger("fadeOut");
        
        
        m_cough_0 = true;
        m_cough_1 = true;
        m_cough_2 = true;
    }
}
