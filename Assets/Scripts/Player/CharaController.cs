using UnityEngine;

public class CharaController : MonoBehaviour
{
    private CharacterController m_characterController;

    [SerializeField, Tooltip("Vitesse du personnage")]
    public float m_speed = 10f;
    [SerializeField, Tooltip("Gravité du personnage")]
    private float m_gravity = -9.81f;

    private Vector3 velocity;

    [SerializeField, Tooltip("GroundCheck du personnage")]
    private Transform m_groundCheck;
    [SerializeField, Tooltip("Distance entre les pieds et le sol")]
    private float m_groundDistance = 0.4f;
    [SerializeField, Tooltip("Layer du sol")]
    private LayerMask m_groundLayer;

    private bool m_isGrounded;

    [SerializeField]
    private PlayerController m_playerController;

    public bool m_isCrouching;

    private void Awake()
    {
        m_playerController = GetComponent<PlayerController>();
        m_characterController = GetComponent<CharacterController>();
    }

    public void UpdateMove()
    {
        m_isGrounded = Physics.CheckSphere(m_groundCheck.position, m_groundDistance, m_groundLayer);
        if (m_isGrounded && velocity.y < 0)
            velocity.y = -2f;
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        // Vitesse divisée par 3 si accroupi
        if(m_isCrouching)
            m_characterController.Move(move * (m_speed / 3) * Time.deltaTime);
        else
            m_characterController.Move(move * m_speed * Time.deltaTime);
    }

    public void UpdateGravity()
    {
        velocity.y += m_gravity * Time.deltaTime;

        m_characterController.Move(velocity * Time.deltaTime);
    }

    public void Crouch()
    {
        m_isCrouching = !m_isCrouching;
    }
}
