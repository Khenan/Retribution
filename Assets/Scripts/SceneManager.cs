using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : Singleton<SceneManager>
{

    [SerializeField, Tooltip("Slider de loading")]
    private GameObject m_canvasLoader;

    private void Start()
    {
        ChangeScene("Game");
    }

    void ChangeScene(string p_sceneName)
    {
        // fait une animation en fade out
        // met en pause la scene courante
        LoadScene(p_sceneName);
    }

    void LoadScene(string p_sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Loader");
        StartCoroutine(LoadAsync(p_sceneName));
    }

    IEnumerator LoadAsync(string p_sceneName)
    {
        AsyncOperation asynOp = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(p_sceneName);

        while (!asynOp.isDone)
        {
            Debug.Log(asynOp.progress);
            yield return null;
        }
    }

    protected override string GetSingletonName()
    {
        return "SceneManager";
    }
}
