using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TableauPupitre : MonoBehaviour
{
    [SerializeField, Tooltip("Symbole Vaudou 1")]
    private GameObject[] m_symbolesVaudou = new GameObject[5];

    [SerializeField, Tooltip("Texte des phrases du tableau à craie")]
    private TextMeshPro m_TMPRO;

    private void Awake()
    {
        if (m_symbolesVaudou.Length < 5)
        {
            Debug.LogWarning("Les 5 symboles vaudou ne sont pas dans le tableau");
        }
    }

    public void Reset()
    {
        m_TMPRO.text = "";
        foreach (GameObject go in m_symbolesVaudou)
        {
            go.SetActive(false);
        }
    }
}
