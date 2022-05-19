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
        
        InteractionController interactCtrl = FindObjectOfType<InteractionController>();
        PlayerController playerCtrl = FindObjectOfType<PlayerController>();
        // Si la porte peut s'ouvrir
        if (!interactCtrl.m_firstOpenDoor)
        {
            interactCtrl.m_firstOpenDoor = true;
            playerCtrl.AnimOpenFirst();
        }
        else
            playerCtrl.AnimOpen();
        
        // On ouvre la porte
        StartCoroutine(CoroutineOpen());
    }
    

    IEnumerator CoroutineOpen()
    {
        yield return new WaitForSeconds(0.5f);
        if (m_leftTrigger)
        {
            m_myDoor.Toggle();
            yield break;
        }
        m_myDoor.Toggle(false);
    }

    public override void Shine()
    {
        m_myDoor.Shine();
    }
}
