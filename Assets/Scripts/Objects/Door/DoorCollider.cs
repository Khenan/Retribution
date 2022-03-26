using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCollider : MonoBehaviour, IInteractible
{
    public float Cooldown { get; set; }
    public List<Renderer> ObjectRendererToShine { get; }
    public bool Shinning { get; set; }
    public int IdShinning { get; set; }
    
    
    [SerializeField, Tooltip("Porte Parente")]
    private Door m_myDoor;

    [SerializeField, Tooltip("Trigger de gauche")]
    private bool m_leftTrigger = true;
    public void Interact()
    {
        if (m_myDoor.m_isOpen) return;
        if (m_leftTrigger)
        {
            m_myDoor.Toggle();
            return;
        }
        m_myDoor.Toggle(false);
    }

    public void Shine()
    {
        m_myDoor.Shine();
    }

    public IEnumerator CooldownCoroutine()
    {
        yield return 0;
    }
}
