using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Text_name", menuName = "ScriptableObject/Text", order = 0)]
public class Text : ScriptableObject
{
    [Header("Phrases en différentes langues [0: ENGLISH, 1: FRENCH]")]
    [SerializeField, Tooltip("Phrases en différentes langues \n 0: ENGLISH \n 1: FRENCH")]
    public List<string> m_Sentences;
}
