using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GlobalVolumeBlendLUT : MonoBehaviour
{
    [SerializeField, Tooltip("GlobalVolume Profile")] 
    private VolumeProfile m_globalVolume;

    [SerializeField] Texture2D m_LUTSelected;
    private ColorLookup m_colorLookUp;

    private float m_contribution = 0f;
    private float m_contributionStep = 0.01f;

    private void OnEnable()
    {
        if (!m_globalVolume.TryGet(out m_colorLookUp)) throw new System.NullReferenceException(nameof(m_colorLookUp));
        m_colorLookUp.texture.Override(m_LUTSelected);
    }

    public void SetLUTTable(Texture2D p_LUTTable)
    {
        m_LUTSelected = p_LUTTable;
    }

    public void StartBlend(bool p_in = true)
    {
        Debug.Log(p_in ? "Ease-In" : "Ease-Out");
        m_colorLookUp.texture.Override(m_LUTSelected);
        StopAllCoroutines();
        StartCoroutine(BlendColorLookUpCoroutine(p_in));
    }

    IEnumerator BlendColorLookUpCoroutine(bool p_in = true)
    {
        yield return new WaitForSeconds(0.01f);

        if (p_in)
        {
            m_contribution += m_contributionStep;
        }
        else
        {
            m_contribution -= m_contributionStep;
        }
        m_colorLookUp.contribution.Override(m_contribution);

        if (p_in && m_contribution < 1)
        {
            StartCoroutine(BlendColorLookUpCoroutine());
        } else if (!p_in && m_contribution > 0)
        {
            StartCoroutine(BlendColorLookUpCoroutine(false));
        }
    }
}
