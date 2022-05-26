using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalFire : MonoBehaviour
{
    [SerializeField, Tooltip("Event à écouter pour Up")] private Event m_eventToUp;
    [SerializeField, Tooltip("Fire Room Pupitre")] private FireRoom m_fireRoomPupitres;
    [SerializeField, Tooltip("Fire Room Horloge")] private FireRoom m_fireRoomHorloge;
    [SerializeField, Tooltip("Transform Final Fire")] private Transform m_transformFinalFire;

    private void OnEnable()
    {
        m_transformFinalFire.gameObject.SetActive(false);
        if(m_eventToUp != null) m_eventToUp.m_event += Up;
    }

    private void OnDisable()
    {
        if(m_eventToUp != null) m_eventToUp.m_event -= Up;
    }
    
    private void Up()
    {
        m_fireRoomPupitres.gameObject.SetActive(true);
        m_fireRoomHorloge.gameObject.SetActive(true);
        m_fireRoomPupitres.Up();
        m_fireRoomHorloge.Up();
        m_fireRoomHorloge.gameObject.SetActive(false);
        m_transformFinalFire.gameObject.SetActive(true);
    }
}
