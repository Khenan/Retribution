using UnityEngine;

public class SceneButton : MonoBehaviour
{
    public void ChangeScene(int p_sceneId)
    {
        SceneManager.Instance.ChangeScene(p_sceneId);
    }
}
