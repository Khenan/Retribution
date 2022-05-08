using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    [SerializeField, Tooltip("Layer du player")] private LayerMask m_playerLayer;
    private void OnTriggerEnter(Collider other)
    {
        if ((m_playerLayer.value & 1<< other.gameObject.layer) > 0)
        {
            other.gameObject.GetComponent<PlayerController>().Death();
        }
    }
}
