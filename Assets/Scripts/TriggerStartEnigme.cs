using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerStartEnigme : MonoBehaviour
{
    [SerializeField, Tooltip("L'énigme à déclencher")]
    private GameObject m_myEnigmeToStart;
    
    [SerializeField, Tooltip("Les autres trigger de la pièce de l'énigme")]
    private List<TriggerStartEnigme> m_myTriggers = new List<TriggerStartEnigme>();
    [SerializeField, Tooltip("Porte(s) de l'énigme")]
    private List<Door> m_myDoors = new List<Door>();

    [SerializeField, Tooltip("Layer du Player")]
    private LayerMask m_playerLayer;

    private void OnTriggerEnter(Collider other)
    {
        if ((m_playerLayer.value & 1 << other.gameObject.layer) <= 0) return;
        // Fermeture de toutes les portes
        foreach (Door door in m_myDoors)
        {
            door.Close();
        }
        // Lancement de l'énigme
        m_myEnigmeToStart.GetComponent<IEnigme>().StartEnigme();
        
        // Désactivation
        foreach (TriggerStartEnigme trigger in m_myTriggers)
        {
            trigger.gameObject.SetActive(false);
        }
        gameObject.SetActive(false);
    }
}
