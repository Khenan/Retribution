using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T m_instance = null;
    [SerializeField] [Tooltip("Destruction au changement de scene ?")] private bool m_dontDestroyOnLoad = true;

    public static T Instance
    {
        get
        {
            if (m_instance == null)
            {
                FindOrCreateInstance();
            }
            return m_instance;
        }
    }

    private static void FindOrCreateInstance()
    {
        m_instance = FindObjectOfType<T>();
        if (m_instance != null)
        {
            (m_instance as Singleton<T>).SetSingleton();
            return;
        }
        
        GameObject go = new GameObject();
        m_instance = go.AddComponent<T>();

        (m_instance as Singleton<T>).SetSingleton(true);
    }

    protected virtual void SetSingleton(bool p_needRename = false)
    {
        if(m_dontDestroyOnLoad)
            DontDestroyOnLoad(gameObject);
        if(p_needRename)
            gameObject.name = GetSingletonName();
    }
    protected abstract string GetSingletonName();

    private void Awake()
    {
        if (m_instance != null)
        {
            if (m_instance != this)
            {
                Destroy(this.gameObject);
                Debug.LogWarning("Plusieurs objets de Singleton !!!");
            }
            return;
        }
        FindOrCreateInstance();
    }
}