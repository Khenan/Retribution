using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField, Tooltip("Vitesse de défilment")] private float m_scrollSpeed = 1;
    [SerializeField, Tooltip("Position de départ en Y")] private float m_startYPos = 0;
    [SerializeField, Tooltip("Position de fin en Y")] private float m_endYPos = 0;

    private void Start()
    {
        transform.position = new Vector3(transform.position.x, m_startYPos, transform.position.z);
    }

    void Update()
    {
        if (transform.position.y < m_endYPos)
            transform.Translate(new Vector3(0, m_scrollSpeed * Time.deltaTime, 0));
        else
            FindObjectOfType<EndCredits>().End();
    }
    
}
