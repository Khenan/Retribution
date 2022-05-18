using System;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField, Tooltip("Player")]
    private PlayerController m_playerCtrl;
    [SerializeField, Tooltip("Player Layer")]
    private LayerMask m_playerLayer;

    public bool m_repop = false;
    public bool m_finish = false;

    [SerializeField, Tooltip("ActiveOnStart")]
    private bool m_activeOnStart = true;
    
    [SerializeField, Tooltip("Event à écouter pour activé le box collider")] private Event m_eventToActive;

    private void OnEnable()
    {
        if (m_activeOnStart) return;
        GetComponent<BoxCollider>().enabled = false;
        m_eventToActive.m_event += Active;
    }
    private void OnDisable()
    {
        if (m_activeOnStart) return;
        m_eventToActive.m_event -= Active;
    }

    private void Active()
    {
        GetComponent<BoxCollider>().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((m_playerLayer & 1 << other.gameObject.layer) <= 0) return;
        if (!m_playerCtrl)
        {
            Debug.LogWarning("Pas de référence du joueur !", this);
            return;
        }
        m_playerCtrl.SetCheckpoint(this);
        GetComponent<BoxCollider>().enabled = false;
        if (!m_repop) return;
        m_finish = true;
    }
}
