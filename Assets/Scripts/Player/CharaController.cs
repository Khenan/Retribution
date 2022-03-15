using UnityEngine;

public class CharaController : MonoBehaviour
{
    private CharacterController m_characterController;

    [SerializeField, Tooltip("Vitesse du personnage")]
    private float m_speed = 10f;
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

    private void Awake()
    {
        m_characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        m_isGrounded = Physics.CheckSphere(m_groundCheck.position, m_groundDistance, m_groundLayer);
        if (m_isGrounded && velocity.y < 0)
            velocity.y = -2f;
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        m_characterController.Move(move * m_speed * Time.deltaTime);

        velocity.y += m_gravity * Time.deltaTime;

        m_characterController.Move(velocity * Time.deltaTime);
    }
}
