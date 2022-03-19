using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendLUT : MonoBehaviour
{
    private GlobalVolumeBlendLUT m_GlobalVolume;
    [SerializeField, Tooltip("LUT Table")]
    private Texture2D m_myLutTable;

    private void Awake()
    {
        m_GlobalVolume = FindObjectOfType<GlobalVolumeBlendLUT>();
        Debug.Log(m_GlobalVolume);
        GetComponent<BoxCollider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
        m_GlobalVolume.SetLUTTable(m_myLutTable);
        m_GlobalVolume.StartBlend();
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit");
        m_GlobalVolume.StartBlend(false);
    }
}
