using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Totem : InteractibleObject
{
    [SerializeField, Tooltip("Numéro du totem"), Range(1, 3)]
    private int m_totemNum = 1;
    [SerializeField, Tooltip("L'objet peut-il être prit ?")]
    private bool m_takable = true;

    public bool m_isTake = false;
    
    public override bool Takable { get => m_takable; set => m_takable = value; }

    private Vector3 m_initPos;
    private Transform m_initParent;
    private Quaternion m_initQuaternion;
    private Vector3 m_initScale;
    
    
    [SerializeField, Tooltip("Animator du Mesh")]
    private Animator m_animator;
    // Animation du totem quand il se brise
    private readonly int m_breakAnimator = Animator.StringToHash("break");

    [SerializeField, Tooltip("Event à lire")]
    private Event m_eventToRead;

    private void OnEnable()
    {
        var tr = transform;
        m_initPos = tr.position;
        m_initParent = tr.parent;
        m_initQuaternion = tr.rotation;
        m_initScale = tr.localScale;
    }

    public void Reset()
    {
        m_isTake = false;
        m_takable = false;
        GetComponent<BoxCollider>().enabled = true;
        
        Debug.Log(m_initPos.x);
        var tr = transform;
        tr.position = m_initPos;
        tr.SetParent(m_initParent);
        tr.rotation = m_initQuaternion;
        tr.localScale = m_initScale;
        
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
        GetComponent<BoxCollider>().enabled = false;
        m_isTake = true;
        if (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.layer = LayerMask.NameToLayer("Overlay");
                if (transform.GetChild(i).childCount <= 0) continue;
                for (int j = 0; j < transform.childCount; j++)
                {
                    transform.GetChild(i).GetChild(j).gameObject.layer = LayerMask.NameToLayer("Overlay");
                }
            }
        }

        StartCoroutine(BreakTotem());
    }

    IEnumerator BreakTotem()
    {
        yield return new WaitForSeconds(2);
        m_animator.SetTrigger(m_breakAnimator);
        GameManager.Instance.BreakTotem(m_totemNum);

        if(m_eventToRead != null) m_eventToRead.Raise();
    }
}
