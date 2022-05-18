using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CameraController)), RequireComponent(typeof(CharaController)), RequireComponent(typeof(InteractionController))]
public class PlayerController : MonoBehaviour
{
    public CameraController m_cameraController;
    private CharaController m_charaController;
    public InteractionController m_interactionController;
    private Suffox m_suffox;
    private PlayerSound m_playerSound;

    private Coroutine coroutineRespawn = null;
    private Coroutine coroutineDeath = null;

    public Checkpoint m_lastCheckpoint;

    private bool m_isDead = false;

    [SerializeField, Tooltip("Le checkpoint de départ")] private Transform m_startCheckpoint = null;

    [SerializeField, Tooltip("Event de lancement de la music d'ambiance")] private Event m_stopAmbiantMusicEvent;
    [SerializeField, Tooltip("Event d'arrêt de la music d'ambiance")] private Event m_playAmbiantMusicEvent;
    
    [Tooltip("Animator Component des bras du personnage")]
    public Animator m_armsAnimator;
    [Tooltip("Animator Component de la caméra du personnage")]
    public Animator m_cameraAnimator;

    private int m_animHash_jumpscare = Animator.StringToHash("jumpscare");
    private int m_animHash_openFirst = Animator.StringToHash("openFirst");
    private int m_animHash_open = Animator.StringToHash("open");
    private int m_animHash_take = Animator.StringToHash("take");
    
    private int m_animHash_start = Animator.StringToHash("start");
    private int m_animHash_dead = Animator.StringToHash("dead");
    private int m_animHash_reset = Animator.StringToHash("reset");

    private void Awake()
    {
        m_cameraController = GetComponent<CameraController>();
        m_charaController = GetComponent<CharaController>();
        m_interactionController = GetComponent<InteractionController>();
        m_suffox = GetComponent<Suffox>();
        m_playerSound = GetComponent<PlayerSound>();

        if (m_startCheckpoint == null) return;
        transform.position = m_startCheckpoint.position;
        transform.rotation = m_startCheckpoint.rotation;
    }

    private void Start()
    {
        m_playAmbiantMusicEvent.Raise();
    }

    private void Update()
    {
        
        m_charaController.UpdateGravity();

        if (!m_isDead && !GameManager.Instance.m_inGameMenu)
        {
            //if (Input.GetKeyDown(KeyCode.G)) Death();
            if (Input.GetKeyDown(KeyCode.C))
            {        
                m_charaController.Crouch();
                m_cameraController.CrouchStand(m_charaController.m_isCrouching);
            }
            m_charaController.UpdateMove();
            m_interactionController.UpdateInteraction();
            m_cameraController.UpdateCamera();
        }

        if (Input.GetKeyDown(KeyCode.G)) AnimJumpscare();
        if (Input.GetKeyDown(KeyCode.H)) AnimOpen();
        if (Input.GetKeyDown(KeyCode.J)) AnimOpenFirst();
        if (Input.GetKeyDown(KeyCode.K)) AnimTake();
        
        m_playerSound.UpdateSound();
    }

    public void Death()
    {
        if (m_isDead) return;
        m_isDead = true;
        Debug.Log("Le joueur est mort");

        m_playerSound.Dead();
        AnimDeadCamera();
        m_stopAmbiantMusicEvent.Raise();
        StartCoroutine(GameOver());
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(3f);
        
        // On stop la coroutine si elle existe
        if(coroutineRespawn != null) StopCoroutine(coroutineRespawn);
        coroutineRespawn = StartCoroutine(RespawnCooldownCoroutine());

        // Fondu au noir apparait
        UIManager.Instance.FadeIn();
    }
    
    private void Respawn()
    {
        m_playAmbiantMusicEvent.Raise();
        // Il récupère son oxygène
        m_suffox.RecoverAllOxygen();

        // On reset la caméra
        AnimResetCamera();
        
        // On le renvoi au dernier checkpoint
        CharacterController characterController = GetComponent<CharacterController>();
        CharaController charaController = GetComponent<CharaController>();
        
        characterController.enabled = false;
        if (charaController.m_isCrouching)
        {
            charaController.Crouch();
            m_cameraController.CrouchStand(m_charaController.m_isCrouching);
        }
        transform.position = m_lastCheckpoint ? m_lastCheckpoint.transform.position : Vector3.zero;
        transform.rotation = m_lastCheckpoint ? m_lastCheckpoint.transform.rotation : Quaternion.identity;
        characterController.enabled = true;
        
        // On restart les énigmes
        EnigmePupitre.Instance.RestartEnigme();
        EnigmeHorloge.Instance.RestartEnigme();
        
        coroutineDeath = StartCoroutine(ResetDeathCoroutine(1));
        
        // Fondu au noir disparait
        UIManager.Instance.FadeOut();
    }
    
    public void SetCheckpoint(Checkpoint p_checkpoint)
    {
        m_lastCheckpoint = p_checkpoint;
        Debug.Log($"Mon checkpoint est {m_lastCheckpoint.name}");
    }

    IEnumerator RespawnCooldownCoroutine(float p_waitSeconds = 2)
    {
        yield return new WaitForSeconds(p_waitSeconds);
        Respawn();
        UIManager.Instance.CloseUIInGame();
    }
    IEnumerator ResetDeathCoroutine(float p_waitSeconds = 2)
    {
        yield return new WaitForSeconds(p_waitSeconds);
        Debug.Log("Le joueur peut jouer");
        m_isDead = false;
    }

    public void AnimJumpscare()
    {
        m_armsAnimator.SetTrigger(m_animHash_jumpscare);
    }
    public void AnimTake()
    {
        m_armsAnimator.SetTrigger(m_animHash_take);
    }
    public void AnimOpen()
    {
        m_armsAnimator.SetTrigger(m_animHash_open);
    }
    public void AnimOpenFirst()
    {
        m_armsAnimator.SetTrigger(m_animHash_openFirst);
    }
    
    public void AnimStartCamera()
    {
        m_cameraAnimator.SetTrigger(m_animHash_start);
    }
    private void AnimDeadCamera()
    {
        m_cameraAnimator.SetTrigger(m_animHash_dead);
    }
    private void AnimResetCamera()
    {
        m_cameraAnimator.SetTrigger(m_animHash_reset);
    }
}
