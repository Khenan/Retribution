using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockMechanism : InteractibleObject
{
    [SerializeField, Tooltip("Animator du mesh")]
    private Animator m_meshAnimator;

    private int m_animatorPushHash = Animator.StringToHash("push");

    private bool m_isPush = false;
    [SerializeField, Tooltip("Le bouton est-t-il bloqué")]
    private bool m_isLock = false;

    public override void Interact()
    {
        if (m_isPush || m_isLock) return;
        m_isPush = true;
        m_meshAnimator.SetTrigger(m_animatorPushHash);
        PushButton();
    }

    public override void Shine()
    {
        if (m_isPush || m_isLock) return;
        base.Shine();
    }

    private void PushButton()
    {
        Debug.Log("Button is Push");
    }
}
