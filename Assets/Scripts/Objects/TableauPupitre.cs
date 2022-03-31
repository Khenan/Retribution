using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableauPupitre : MonoBehaviour
{
    [SerializeField, Tooltip("Symbole Vaudou 1")]
    private GameObject[] m_symbolesVaudou = new GameObject[5];

    private void Awake()
    {
        if (m_symbolesVaudou.Length < 5)
        {
            Debug.LogWarning("Les 5 symboles vaudou ne sont pas dans le tableau");
        }
    }
}
