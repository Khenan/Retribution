using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class InteractionController : MonoBehaviour
{
    [Header("INTERACTION")]
    [SerializeField, Tooltip("Départ du rayon d'intéraction")]
    private Transform m_rootTransform;
    [SerializeField, Tooltip("Portée du joueur en mètre")]
    private float m_range = 2;
    
    [Header("LAYERS")]
    [SerializeField, Tooltip("Tous Layers sauf Player")]
    private LayerMask m_noPlayerLayer;
    [SerializeField, Tooltip("Layer des objets interactifs")]
    private LayerMask m_interactLayer;
    
    [Header("OBJET")]
    [SerializeField, Tooltip("Crouch position")]
    private Transform m_crouchPos;
    [SerializeField, Tooltip("Stand position")]
    private Transform m_standPos;
    [SerializeField, Tooltip("Emplacement de la main quand le joueur tient un objet")]
    private TwoBoneIKConstraint m_handIK;
    [SerializeField, Tooltip("Emplacement d'objet à tenir")]
    private Transform m_objectPos;
    [SerializeField, Tooltip("Objet dans la main du joueur")]
    private bool m_handFull = false;

    private void Awake()
    {
        m_handIK.weight = 0;
    }

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
                    if (!m_handFull && hit.collider.gameObject.GetComponent<IInteractible>().Takable)
                    {
                        Take(hit.collider.gameObject);
                    }
                }
            }
        }
    }

    public void Drop()
    {
        m_handFull = false;
        StartCoroutine(MoveHandIdleCoroutine());
    }

    private void Take(GameObject m_myObjectInteractible)
    {
        m_handFull = true;
        m_myObjectInteractible.transform.SetParent(m_objectPos);
        m_myObjectInteractible.transform.position = m_objectPos.position;
        m_myObjectInteractible.transform.rotation = m_objectPos.rotation;

        StartCoroutine(MoveHandCoroutine());
    }

    IEnumerator MoveHandCoroutine()
    {
        while (m_handIK.weight < 1)
        {
            m_handIK.weight += 0.03f;
            yield return new WaitForSeconds(0.01f);
        }
    }
    IEnumerator MoveHandIdleCoroutine()
    {
        while (m_handIK.weight > 0)
        {
            m_handIK.weight -= 0.03f;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
