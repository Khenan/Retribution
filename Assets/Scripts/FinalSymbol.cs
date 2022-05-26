using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalSymbol : MonoBehaviour
{
    [SerializeField, Tooltip("Event à écouter pour apparaitre")] private Event m_EventToListen_pop;
    [SerializeField, Tooltip("Temps en seconde avant de pop après l'event")] private float m_wait;
    [SerializeField, Tooltip("Sound Event pour POP")] private SoundEvent m_soundEventPop;

    private void OnEnable()
    {
        m_EventToListen_pop.m_event += Pop;
        GetComponent<Animator>().SetTrigger("disappear");
    }

    private void OnDisable()
    {
        m_EventToListen_pop.m_event -= Pop;
    }

    private void Pop()
    {
        StartCoroutine(PopCoroutine());
    }

    IEnumerator PopCoroutine()
    {
        yield return new WaitForSeconds(m_wait);
        m_soundEventPop.Stop();
        m_soundEventPop.Play();
        GetComponent<Animator>().SetTrigger("pop");
    }
}
