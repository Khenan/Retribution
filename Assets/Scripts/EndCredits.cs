using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCredits : MonoBehaviour
{
    private bool m_end = false;
    public Animator m_animFade;
    public void End()
    {
        if (m_end) return;
        m_end = true;
        m_animFade.SetTrigger("fadeOut");
        StartCoroutine(EndCoroutine());
    }

    IEnumerator EndCoroutine()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.Instance.ChangeSceneDirect(1);
    }
}
