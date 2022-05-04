using System.Collections;
using UnityEngine;
public class ChildCorridor : MonoBehaviour
{
    [SerializeField, Tooltip("Event à écouter pour s'enfuir")] private Event m_EventToListen;
    [SerializeField, Tooltip("Delay en seconde de la fermeture de la porte")] private float m_waitTimeSecond = 1f;
    [SerializeField, Tooltip("Porte à fermer")] private Door m_door;

    private int m_triggerAnim = Animator.StringToHash("trigger");
    private Animator m_anim;

    private void OnEnable()
    {
        m_anim = GetComponent<Animator>();
        m_EventToListen.m_event += Trigger;
    }
    private void OnDisable()
    {
        m_EventToListen.m_event -= Trigger;
    }

    private void Trigger()
    {
        m_anim.SetTrigger(m_triggerAnim);
        StartCoroutine(CloseDoorCoroutine());
    }

    IEnumerator CloseDoorCoroutine()
    {
        yield return new WaitForSeconds(m_waitTimeSecond);
        gameObject.SetActive(false);
        m_door.Close();
    }
}
