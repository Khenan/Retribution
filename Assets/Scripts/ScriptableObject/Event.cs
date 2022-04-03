using UnityEngine;
[CreateAssetMenu(fileName = "Event_name", menuName = "ScriptableObject/Event", order = 1)]
public class Event : ScriptableObject
{
    public delegate void MyDelegate();

    public event MyDelegate m_event;
    
    public void Raise()
    {
        m_event?.Invoke();
    }
}
