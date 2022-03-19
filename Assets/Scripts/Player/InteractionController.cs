using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [SerializeField, Tooltip("Départ du rayon d'intéraction")]
    private Transform m_rootTransform;
    [SerializeField, Tooltip("Portée du joueur en mètre")]
    private float m_range = 2;
    [SerializeField, Tooltip("Tous Layers sauf Player")]
    private LayerMask m_noPlayerLayer;
    [SerializeField, Tooltip("Layer des objets interactifs")]
    private LayerMask m_interactLayer;

    public void UpdateInteraction()
    {
        if (Physics.Raycast(m_rootTransform.position, m_rootTransform.forward, out RaycastHit hit, m_range, m_noPlayerLayer))
        {
            if ((m_interactLayer.value & 1<< hit.collider.gameObject.layer) > 0)
            {
                hit.collider.gameObject.GetComponent<IInteractible>().Shine();
            }
            
            // Si clique droit, on affiche le nom de l'objet
            if (Input.GetMouseButtonDown(0))
            {
                if ((m_interactLayer.value & 1<< hit.collider.gameObject.layer) > 0)
                {
                    print(hit.collider.name);
                    hit.collider.gameObject.GetComponent<IInteractible>().Interact();
                }
            }
        }
    }
}
