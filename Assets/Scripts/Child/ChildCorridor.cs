using System.Collections;
using Unity.Mathematics;
using UnityEngine;
public class ChildCorridor : MonoBehaviour
{
    [SerializeField, Tooltip("Event à écouter pour s'enfuir")] private Event m_EventToListen;
    [SerializeField, Tooltip("Event à écouter pour repop")] private Event m_EventToRepop;
    [SerializeField, Tooltip("Delay en seconde de la fermeture de la porte")] private float m_waitTimeSecond = 1f;
    [SerializeField, Tooltip("Porte à fermer")] private Door m_door;
    [SerializeField, Tooltip("Premiere pos")] private Transform m_firstPos;
    [SerializeField, Tooltip("Deuxieme pos")] private Transform m_secondPos;
    [SerializeField, Tooltip("Deuxieme pos")] private Transform m_waitPos;

    private int m_triggerAnim = Animator.StringToHash("trigger");
    private Animator m_anim;

    [SerializeField, Tooltip("Son de screamer")]
    private SoundEvent m_screamerSound;

    public bool m_repop = false;

    private void OnEnable()
    {
        m_anim = GetComponent<Animator>();
        transform.parent = m_firstPos;
        transform.localPosition = Vector3.zero;
        transform.rotation = quaternion.identity;
        m_EventToListen.m_event += Trigger;
        m_EventToRepop.m_event += Repop;
    }
    private void OnDisable()
    {
        m_EventToListen.m_event -= Trigger;
        m_EventToRepop.m_event -= Repop;
    }

    private void Trigger()
    {
        m_screamerSound.Play();
        m_anim.SetTrigger(m_triggerAnim);
        StartCoroutine(CloseDoorCoroutine());
    }

    private void Repop()
    {
        m_repop = true;
        transform.parent = m_secondPos;
        transform.localPosition = Vector3.zero;
        transform.rotation = quaternion.identity;
        m_anim.ResetTrigger(m_triggerAnim);
    }

    IEnumerator CloseDoorCoroutine()
    {
        yield return new WaitForSeconds(m_waitTimeSecond);
        transform.position = m_waitPos.position;
        if (!m_repop)
            m_door.Close();
    }
}
