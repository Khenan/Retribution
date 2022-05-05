using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class FireRoom : MonoBehaviour
{
    [SerializeField, Tooltip("List des feux à s'élever en premier")] private Transform m_firstFires;
    [SerializeField, Tooltip("List des feux à s'élever en deuxième")] private Transform m_secondFires;
    [SerializeField, Tooltip("List des feux à s'élever en dernier")] private Transform m_lastFires;

    [SerializeField, Tooltip("Event à écouter pour les premiers feux")] private Event m_firstFireEvent;
    [SerializeField, Tooltip("Event à écouter pour reset")] private Event m_eventToReset;

    [SerializeField] private float m_step = 0.05f;

    private Vector3 m_init_posFirstFires;
    private Vector3 m_init_posSecondFires;
    private Vector3 m_init_posLastFires;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        m_init_posFirstFires = new Vector3(m_firstFires.position.x, m_firstFires.position.y, m_firstFires.position.z);
        m_init_posSecondFires = new Vector3(m_secondFires.position.x, m_secondFires.position.y, m_secondFires.position.z);
        m_init_posLastFires = new Vector3(m_lastFires.position.x, m_lastFires.position.y, m_lastFires.position.z);
        m_firstFireEvent.m_event += FirstHandle;
        m_eventToReset.m_event += Reset;
    }

    private void OnDisable()
    {
        m_firstFireEvent.m_event -= FirstHandle;
        m_eventToReset.m_event -= Reset;
    }

    private void FirstHandle()
    {
        StartCoroutine(UpFireCoroutine(m_firstFires));
    }
    private void SecondHandle()
    {
        StartCoroutine(DelaySecondFireCoroutine(20, m_secondFires));
    }
    private void LastHandle()
    {
        StartCoroutine(DelaySecondFireCoroutine(20, m_lastFires));
    }

    IEnumerator DelaySecondFireCoroutine(float p_second, Transform p_fire)
    {
        yield return new WaitForSeconds(p_second);
        StartCoroutine(UpFireCoroutine(p_fire));
    }

    IEnumerator UpFireCoroutine(Transform p_fire)
    {
        Vector3 pos = p_fire.localPosition;
        if (pos.y >= 0) SecondHandle();
        if (!(pos.y < 0)) yield break;
        yield return new WaitForSeconds(0.2f);
        p_fire.localPosition = new Vector3(pos.x, pos.y + m_step, pos.z);
        StartCoroutine(UpFireCoroutine(p_fire));
    }

    public void Reset()
    {
        StopAllCoroutines();
        m_firstFires.position = m_init_posFirstFires;
        m_secondFires.position = m_init_posSecondFires;
        m_lastFires.position = m_init_posLastFires;
    }
}
