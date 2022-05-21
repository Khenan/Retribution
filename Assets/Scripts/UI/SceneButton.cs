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
        m_leftIcon.gameObject.SetActive(false);
        m_rightIcon.gameObject.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        m_leftIcon.gameObject.SetActive(true);
        m_rightIcon.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_leftIcon.gameObject.SetActive(false);
        m_rightIcon.gameObject.SetActive(false);
    }
}
