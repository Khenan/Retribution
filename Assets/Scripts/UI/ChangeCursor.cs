using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCursor : MonoBehaviour
{
    [SerializeField, Tooltip("Texture du Curseur")]
    private Texture2D m_cursorTexture2D;

    [SerializeField, Tooltip("Texture du Curseur")]
    private bool m_centerCursor = false;

    private void Start()
    {
        Change();
    }

    private void Change()
    {
        Cursor.lockState = CursorLockMode.Confined;
        if(m_centerCursor)
            Cursor.SetCursor(m_cursorTexture2D, new Vector2(m_cursorTexture2D.width / 2, m_cursorTexture2D.height / 2), CursorMode.Auto);
        else
            Cursor.SetCursor(m_cursorTexture2D, Vector2.zero, CursorMode.Auto);
    }
}
