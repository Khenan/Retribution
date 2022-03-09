using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Loader : MonoBehaviour
{
    public GameObject m_loadSlider;
    public TextMeshProUGUI m_loadText;

    private void OnEnable()
    {
        SceneManager.Instance.m_loadSlider = m_loadSlider.GetComponent<Slider>();
        SceneManager.Instance.m_loadText = m_loadText;
        SceneManager.Instance.LoadScene();
    }
}
