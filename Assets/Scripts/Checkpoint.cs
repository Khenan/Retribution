using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField, Tooltip("Player")]
    private PlayerController m_playerCtrl;
    [SerializeField, Tooltip("Player Layer")]
    private LayerMask m_playerLayer;

    public bool m_repop = false;
    public bool m_finish = false;

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
