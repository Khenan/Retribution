using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerStartEnigme : MonoBehaviour
{
    [SerializeField, Tooltip("Les autres trigger de la pièce de l'énigme")]
    private List<TriggerStartEnigme> m_myTriggers = new List<TriggerStartEnigme>();
    [SerializeField, Tooltip("Porte(s) de l'énigme")]
    private List<Door> m_myDoors = new List<Door>();

    [SerializeField, Tooltip("Layer du Player")]
    private LayerMask m_playerLayer;

    private void OnTriggerEnter(Collider other)
    {
        print("Dans l'énigme, fermeture des portes");
        if ((m_playerLayer.value & 1 << other.gameObject.layer) <= 0) return;
        foreach (Door door in m_myDoors)
        {
            door.Close();
        }
        foreach (TriggerStartEnigme trigger in m_myTriggers)
        {
            trigger.gameObject.SetActive(false);
        }
        
        gameObject.SetActive(false);
    }
}
