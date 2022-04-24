using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : InteractibleObject
{
    [SerializeField, Tooltip("L'objet peut-il être prit ?")]
    private bool m_takable = false;

    public bool m_isTake = false;
    
    public bool Takable { get => m_takable; set => m_takable = value; }

    private Vector3 m_initPos;
    private Transform m_initParent;
    private Quaternion m_initQuaternion;
    
    
    [SerializeField, Tooltip("Animator du Mesh")]
    private Animator m_animator;
    // Animation du totem quand il se brise
    // private readonly int m_breakAnimator = Animator.StringToHash("break");

    private void OnEnable()
    {
        var tr = transform;
        m_initPos = tr.position;
        m_initParent = tr.parent;
        m_initQuaternion = tr.rotation;
    }

    public void Reset()
    {
        m_isTake = false;
        m_takable = false;
        
        Debug.Log(m_initPos.x);
        var tr = transform;
        tr.position = m_initPos;
        tr.SetParent(m_initParent);
        tr.rotation = m_initQuaternion;
        
        gameObject.layer = LayerMask.NameToLayer("Interact");
        if (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.layer = LayerMask.NameToLayer("Interact");
            }
        }
        GameManager.Instance.m_playerCtrl.m_interactionController.Drop();
    }

    public override void Interact()
    {
        gameObject.layer = LayerMask.NameToLayer("Overlay");
        m_isTake = true;
        if (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.layer = LayerMask.NameToLayer("Overlay");
            }
        }
    }
}
