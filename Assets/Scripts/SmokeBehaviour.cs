using System;
using UnityEngine;
using UnityEngine.VFX;

public class SmokeBehaviour : MonoBehaviour
{
    [SerializeField, Tooltip("Position en Y de départ de la fumée, placez la fumée à la position de départ désirée et inscrivez la valeur dans le champs")]
    private float m_minPosY = 0;
    [SerializeField, Tooltip("Plafond de la fumée, placez la fumée à la hauteur maximal qu'elle pourrait aller et inscrivez la valeur dans le champs")]
    private float m_maxPosY = 4;
    [SerializeField, Tooltip("Vitesse de la fumée en Y en m/s"), Range(0, 1)]
    private float m_speedY = 0.01f;
    
    [HideInInspector]
    public Transform m_smoke;

    [HideInInspector]
    public bool m_start = false;

    private float m_currentTime = 0;

    private void Awake()
    {
        m_smoke = transform.GetChild(0);
        Init();
    }

    private void Start()
    {
        // Sécurité supplémentaire
        Init();
    }

    private void FixedUpdate()
    {
        if(!m_start) return;
        m_currentTime += Time.deltaTime;
        transform.Translate(0, 1f * m_speedY * Time.deltaTime, 0);
        if (transform.localPosition.y < m_maxPosY) return;
        m_start = false;
        Debug.Log("Fumée arrivée en " + m_currentTime + "s.");
    }

    public void Begin()
    {
        m_start = true;
    }

    public void Restart()
    {
        m_start = false;
        Init();
    }

    private void Init()
    {
        m_smoke.GetComponent<VisualEffect>().Reinit();
        m_smoke.GetComponent<VisualEffect>().playRate = 0;
        transform.localPosition = new Vector3(transform.localPosition.x, m_minPosY, transform.localPosition.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        print("Dedans");
        other.transform.parent.parent.GetComponent<Suffox>().TakeDamage();
    }

    private void OnTriggerExit(Collider other)
    {
        print("Dehors");
        other.transform.parent.parent.GetComponent<Suffox>().RecoverOxygen();
    }
}
