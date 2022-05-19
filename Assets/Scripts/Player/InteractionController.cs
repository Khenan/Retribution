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
    [SerializeField]
    private PlayerController m_playerController;
    
    [Header("LAYERS")]
    [SerializeField, Tooltip("Tous Layers sauf Player")]
    private LayerMask m_noPlayerLayer;
    [SerializeField, Tooltip("Layer des objets interactifs")]
    private LayerMask m_interactLayer;
    [SerializeField, Tooltip("Layer des dessins pour l'horloge")]
    private LayerMask m_clockDrawLayer;
    
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

    public bool m_firstOpenDoor;

    private void Awake()
    {
        m_playerController = GetComponent<PlayerController>();
        m_handIK.weight = 0;
    }

    public void UpdateInteraction()
    {
        if (Physics.Raycast(m_rootTransform.position, m_rootTransform.forward, out RaycastHit hit, m_range, m_noPlayerLayer))
        {
            
            // ----------------- INTERACT ----------------- //
            if ((m_interactLayer.value & 1<< hit.collider.gameObject.layer) > 0 && !m_handFull)
            {
                hit.collider.gameObject.GetComponent<InteractibleObject>().Shine();
            }
            
            // Si clique droit, on affiche le nom de l'objet
            if (Input.GetMouseButtonDown(0) && !m_handFull)
            {
                Interact(hit);
            }
            // ----------------- CLOCK DRAW ----------------- //
            if ((m_clockDrawLayer.value & 1<< hit.collider.gameObject.layer) > 0)
            {
                Look(hit);
            }
        }
    }

    private void Interact(RaycastHit p_hit)
    {
        if ((m_interactLayer.value & 1<< p_hit.collider.gameObject.layer) > 0)
        {
            p_hit.collider.gameObject.GetComponent<InteractibleObject>().Interact();
            if (!m_handFull && p_hit.collider.gameObject.GetComponent<InteractibleObject>().Takable)
            {
                Take(p_hit.collider.gameObject);
            }
        }
    }

    private void Look(RaycastHit p_hit)
    {
        if (!GetComponent<CharaController>().m_isCrouching) return;
        p_hit.collider.gameObject.GetComponent<ClockSymbol>().LookContinue(Time.deltaTime);
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
        m_myObjectInteractible.transform.localPosition = Vector3.zero;
        m_myObjectInteractible.transform.localRotation = Quaternion.Euler(Vector3.zero);
        m_myObjectInteractible.transform.localScale = Vector3.one;
        
        m_playerController.AnimTake();
        StartCoroutine(StartMoveHandCoroutine());
    }

    IEnumerator StartMoveHandCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(MoveHandCoroutine());
        StartCoroutine(ReplaceHandCoroutine());
    }

    IEnumerator MoveHandCoroutine()
    {
        while (m_handIK.weight < 1)
        {
            m_handIK.weight += 0.03f;
            yield return new WaitForSeconds(0.01f);
        }
    }
    IEnumerator ReplaceHandCoroutine()
    {
        yield return new WaitForSeconds(3f);
        m_handFull = false;
        StartCoroutine(MoveHandIdleCoroutine());
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
