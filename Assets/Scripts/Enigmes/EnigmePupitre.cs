using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EnigmePupitre : Singleton<EnigmePupitre>, IEnigme
{
    [SerializeField, Tooltip("Totem de l'énigme")]
    private Totem m_totem;
    
    private int m_lastNum = 0;
    [SerializeField, Tooltip("Numéro Vainqueur du pupitre"), Range(2, 10)]
    public int m_goalNum = 4;
    public delegate void MyDelegate();
    public MyDelegate m_close;
    
    [SerializeField, Tooltip("Le tableau à craie de l'énigme")]
    private TableauPupitre m_chalkboard;

    [SerializeField, Tooltip("Le checkpoint de l'énigme")]
    private Checkpoint m_checkpoint;
    public Checkpoint Checkpoint => m_checkpoint;
    
    [SerializeField, Tooltip("Le déclencheur de l'énigme")]
    private GameObject m_triggerStart;

    [SerializeField, Tooltip("layer pupitres")]
    private LayerMask m_pupitresLayer;

    private bool m_isCompleted = false;
    
    [SerializeField, Tooltip("Le prefab des pupitres")]
    private GameObject m_pupitresPrefab;

    [SerializeField, Tooltip("Fumée de l'énigme")]
    private SmokeBehaviour m_smoke;
    
    [SerializeField, Tooltip("Porte(s) de l'énigme")]
    private List<Door> m_myDoors = new List<Door>();

    private void Awake()
    {
        if (!m_totem)
        {
            Debug.LogWarning("Attention, aucun totem n'est attribué à l'énigme des pupitres", this);
        }
        // Fermeture de toutes les portes
        foreach (Door door in m_myDoors)
        {
            door.Close();
        }
    }

    private void Start()
    {
        Instantiate(m_pupitresPrefab, transform);
        m_totem.Takable = false;
    }

    public void StartEnigme()
    {
        Debug.Log("L'énigme des pupitres commence !!!");
        // Fermeture de toutes les portes
        foreach (Door door in m_myDoors)
        {
            door.Close();
        }

        m_chalkboard.ReadNextSentence();
        
        if (m_smoke)
        {
            m_smoke.m_smoke.GetComponent<VisualEffect>().playRate = 1;
            m_smoke.Begin();
        }
        else
        {
            Debug.LogWarning("Attention, aucune fumée n'a été attribué à l'énigme, elle ne peut donc pas apparaître", this);
        }
    }

    public void RestartEnigme()
    {
        if (m_isCompleted && m_checkpoint.m_finish) return;
        m_checkpoint.gameObject.GetComponent<BoxCollider>().enabled = false;
        
        // Déblocage de toutes les portes
        foreach (Door door in m_myDoors)
        {
            door.Close();
            door.m_isLock = false;
        }

        // On recherche les pupitres dans les enfants de l'énigme
        GameObject pupitres = null;
        for (int i = 0; i < transform.childCount; i++)
        {
            if ((m_pupitresLayer.value & 1 << transform.GetChild(i).gameObject.layer) > 0)
                pupitres = transform.GetChild(i).gameObject;
        }

        if (pupitres != null)
            Destroy(pupitres);
        else
            Debug.LogWarning("Les pupitres n'ont pas été trouvé !", this);
        
        // On replace le totem
        m_totem.Reset();
        
        // on reset le tableau
        m_chalkboard.Reset();
        
        // On reset les variables
        m_lastNum = 0;
        m_isCompleted = false;
        m_triggerStart.SetActive(true);
        Instantiate(m_pupitresPrefab, transform);
        
        if (!m_smoke) return;
        m_smoke.Restart();
    }

    public void CompleteEnigme()
    {
        m_isCompleted = true;
        m_checkpoint.m_repop = true;
        m_checkpoint.gameObject.GetComponent<BoxCollider>().enabled = true;
        
        // On rend récupérable le totem
        m_totem.Takable = true;
        
        // Ouverture de toutes les portes
        foreach (Door door in m_myDoors)
        {
            door.OpenLeft();
        }
    }

    public bool CheckPupitre(int p_numPupitre)
    {
        // Interaction avec le prochain bon pupitre
        if (m_lastNum + 1 == p_numPupitre)
        {
            m_lastNum = p_numPupitre;
            Debug.Log($"{m_lastNum}, {m_chalkboard.m_idSentences}");
            if(m_lastNum > m_chalkboard.m_idSentences)
                m_chalkboard.ReadNextSentence();
            // Interaction avec le dernier bon pupitre
            if (m_lastNum == m_goalNum)
            {
                print("GAGNÉ !!!!");
                CompleteEnigme();
            }
            return true;
        }
        if (m_isCompleted) return false;
        // Interaction avec un mauvais pupitre
        m_lastNum = 0;
        // on reset le tableau
        if (m_chalkboard.m_idSentences > 0)
        {
            m_chalkboard.Reset();
            m_chalkboard.ReadNextSentence();
        }
        m_close?.Invoke();
        print(m_lastNum);
        return false;
    }

    protected override string GetSingletonName()
    {
        return "EnigmePupitre";
    }
}
