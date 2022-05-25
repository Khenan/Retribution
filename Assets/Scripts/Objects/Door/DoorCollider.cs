using System.Collections;
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
        m_myDoor.ClearCollider();
        InteractionController interactCtrl = GameManager.Instance.m_playerCtrl.GetComponent<InteractionController>();
        // Si la porte peut s'ouvrir
        if (!interactCtrl.m_firstOpenDoor)
        {
            interactCtrl.m_firstOpenDoor = true;
            GameManager.Instance.m_playerCtrl.AnimOpenFirst();
        }
        else
            GameManager.Instance.m_playerCtrl.AnimOpen();
        
        // On ouvre la porte
        StartCoroutine(CoroutineOpen());
    }
    

    IEnumerator CoroutineOpen()
    {
        yield return new WaitForSeconds(1.05f);
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
