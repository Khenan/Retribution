using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SceneButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField, Tooltip("Icone gauche")] private Transform m_leftIcon; 
    [SerializeField, Tooltip("Icone droite")] private Transform m_rightIcon; 
    public void ChangeScene(int p_sceneId)
    {
        SceneManager.Instance.ChangeScene(p_sceneId);
    }

    private void OnEnable()
    {
        if (m_leftIcon != null) m_leftIcon.gameObject.SetActive(false);
        if (m_rightIcon != null) m_rightIcon.gameObject.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (m_leftIcon != null) m_leftIcon.gameObject.SetActive(true);
        if (m_rightIcon != null) m_rightIcon.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (m_leftIcon != null) m_leftIcon.gameObject.SetActive(false);
        if (m_rightIcon != null) m_rightIcon.gameObject.SetActive(false);
    }
}
