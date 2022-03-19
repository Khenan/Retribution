using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible : MonoBehaviour, IInteractible
{
    public float m_cooldown = 0.5f;

    public List<Renderer> m_objectRendererToShine;

    private bool m_shinning = false;
    private int m_idShinning = Shader.PropertyToID("_shinning");
    private float _cooldown;

    float IInteractible.Cooldown
    {
        get => _cooldown;
        set => _cooldown = value;
    }

    public List<Renderer> ObjectRendererToShine { get; }
    public bool Shinning { get; set; }
    public int IdShinning { get; set; }

    public void Interact()
    {
        
    }

    public void Shine()
    {
        if (m_shinning) return;
        
        m_shinning = true;
        foreach (Renderer rnd in m_objectRendererToShine)
            rnd.material.SetFloat(m_idShinning, 1);
        
        StartCoroutine(CooldownCoroutine());
    }

    public IEnumerator CooldownCoroutine()
    {
        yield return new WaitForSeconds(m_cooldown);
        m_shinning = false;
        foreach (Renderer rnd in m_objectRendererToShine)
            rnd.material.SetFloat(m_idShinning, 0);
    }
}
