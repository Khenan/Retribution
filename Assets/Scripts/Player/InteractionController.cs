using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    public Transform m_rootTransform;
    public float m_range = 2;
    public LayerMask m_layerInteract;

    public void UpdateInteraction()
    {
        if (Physics.Raycast(m_rootTransform.position, m_rootTransform.forward, out RaycastHit hit, m_range))
        {
            if ((m_layerInteract.value & 1<< hit.collider.gameObject.layer) > 0)
            {
                hit.collider.gameObject.GetComponent<Interactible>().Shine();
            }
            
            // Si clique droit, on affiche le nom de l'objet
            if (Input.GetMouseButtonDown(0))
            {
                if ((m_layerInteract.value & 1<< hit.collider.gameObject.layer) > 0)
                {
                    print(hit.collider.name);
                }
            }
        }
    }
}
