using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : Singleton<SceneManager>
{
    [HideInInspector]
    public Slider m_loadSlider;
    [HideInInspector]
    public TextMeshProUGUI m_loadText;

    public int m_sceneToLoad = 2;

    public void ChangeScene(int p_sceneId)
    {
        m_sceneToLoad = p_sceneId;
        
        // fait une animation en fade out
        // met en pause la scene courante
        
        // Lance la scene de load
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Loader");
    }

    public void LoadScene()
    {
        StartCoroutine(LoadAsync(m_sceneToLoad));
    }

    IEnumerator LoadAsync(int p_sceneId)
    {
        AsyncOperation asynOp = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(p_sceneId);

        while (!asynOp.isDone)
        {
            Debug.Log(asynOp.progress);
            if (m_loadSlider)
            {
                float prct = asynOp.progress * 100;
                m_loadText.text = $"{prct} %";
                m_loadSlider.value = asynOp.progress;
            }
            yield return null;
        }
    }

    protected override string GetSingletonName()
    {
        return "SceneManager";
    }
}
