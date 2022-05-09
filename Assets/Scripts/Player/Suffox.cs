using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Suffox : MonoBehaviour
{
    private PlayerController m_playerCtrl;

    [SerializeField, Tooltip("(1 = 1 seconde) En combien de temps le joueur perd 1 oxygène"), Range(0, 100)]
    private float m_cooldownTakeDamage = 0.1f;
    [SerializeField, Tooltip("(1 = 1 seconde) En combien de temps le joueur récuàère 1 oxygène"), Range(0, 100)]
    private float m_cooldownRecoverOxygen = 0.1f;
    
    [SerializeField, Tooltip("Point d'oxygène du joueur"), Range(0, 100)]
    private float m_oxygenMax = 100;

    public float m_oxygen;
    
    // UI
    [SerializeField, Tooltip("Slider UI d'oxygène")]
    private Slider m_oxygenSlider;
    
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
            StartCoroutine(TakeDamageCoroutine(p_second));
        }

    }
    IEnumerator RecoverOxygenCoroutine(float p_second)
    {
        yield return new WaitForSeconds(p_second);
        UpdateSlider();
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

    private void UpdateSlider()
    {
        if (!m_oxygenSlider)
        {
            Debug.LogWarning("Le slider d'oxygène n'est pas attribué");
            return;
        }

        m_oxygenSlider.value = m_oxygen;
    }
}
