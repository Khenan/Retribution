using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenFInal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        foreach (var t in FindObjectsOfType<LockSymbol>())
        {
            t.gameObject.SetActive(false);
        }
    }
}
