using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;

public class Text3DButton : MonoBehaviour
{
    private TextMeshPro m_tmpro;
    [SerializeField, Tooltip("Couleur par défaut")] private Color m_defaultColor;
    [SerializeField, Tooltip("Couleur au moment du Hover du pointeur")] private Color m_hoverColor;
    [SerializeField, Tooltip("Index de la scène à donner"), Range(0, 3)] private int m_sceneIndex = 0;
    [SerializeField, Tooltip("Le Bloqueur de cursor pour éviter de cliquer sur un bouton après avoir cliqué sur un")] private BoxCollider m_bloqueur;

    [SerializeField] private bool m_quitButton;
    private void Awake()
    {
        m_tmpro = GetComponent<TextMeshPro>();
    }

    private void OnMouseEnter()
    {
        m_tmpro.color = m_hoverColor;
    }
    private void OnMouseExit()
    {
        m_tmpro.color = m_defaultColor;
    }
    private void OnMouseUp()
    {
        if (m_quitButton)
        {
            Application.Quit();
            return;
        }
        m_bloqueur.enabled = true;
        SceneManager.Instance.ChangeScene(m_sceneIndex);
    }
}
