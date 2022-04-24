using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCollider : InteractibleObject
{
    [SerializeField, Tooltip("Porte Parente")]
    private Door m_myDoor;

    [SerializeField, Tooltip("Trigger de gauche")]
    private bool m_leftTrigger = true;
    public override void Interact()
    {
        if (m_myDoor.m_isOpen) return;
        if (m_leftTrigger)
        {
            m_myDoor.Toggle();
            return;
        }
        m_myDoor.Toggle(false);
    }

    public override void Shine()
    {
        m_myDoor.Shine();
    }
}
