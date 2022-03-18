using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible : MonoBehaviour
{
    public float m_cooldown = 0.5f;

    public List<Renderer> m_objectRendererToShine;

    private bool m_shinning = false;
    private int m_idShinning = Shader.PropertyToID("_shinning");

    public void Shine()
    {
        if (m_shinning) return;
        
        m_shinning = true;
        foreach (Renderer rnd in m_objectRendererToShine)
            rnd.material.SetFloat(m_idShinning, 1);
        
        StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(m_cooldown);
        m_shinning = false;
        foreach (Renderer rnd in m_objectRendererToShine)
            rnd.material.SetFloat(m_idShinning, 0);
    }
}
