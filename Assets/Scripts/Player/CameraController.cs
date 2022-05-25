using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField, Tooltip("Sensibilité de la souris sur l'axe X")]
    public float m_mouseSensitivityX = 150f;
    [SerializeField, Tooltip("Sensibilité de la souris sur l'axe Y")]
    public float m_mouseSensitivityY = 150f;
    
    [SerializeField, Tooltip("Vue maximale vers le sol")]
    private float m_minViewY = -70f;
    [SerializeField, Tooltip("Vue maximale vers le ciel")]
    private float m_maxViewY = 70f;

    [SerializeField, Tooltip("Transform de la caméra")]
    private Transform m_cameraTransform;

    private float m_rotationX = 0f;

    [SerializeField, Tooltip("Transform du Root debout")]
    private Transform m_standPos;
    [SerializeField, Tooltip("Transform du Root accroupi")]
    private Transform m_crouchPos;

    [SerializeField, Tooltip("Vitesse d'accroupissement")]
    private float m_speedCrouch = 0.05f;

    public void UpdateCamera()
    {
        float x = Input.GetAxis("Mouse X") * m_mouseSensitivityX * Time.deltaTime;
        float y = Input.GetAxis("Mouse Y") * m_mouseSensitivityY * Time.deltaTime;

        m_rotationX -= y;
        m_rotationX = Mathf.Clamp(m_rotationX, m_minViewY, m_maxViewY);
        
        m_cameraTransform.localRotation = Quaternion.Euler(m_rotationX, 0f, 0f);
        transform.Rotate(Vector3.up * x);
    }

    public void CrouchStand(bool p_crouch = true)
    {
        StopAllCoroutines();
        StartCoroutine(CrouchStandCoroutine(p_crouch));
    }

    IEnumerator CrouchStandCoroutine(bool p_crouch = true)
    {
        if (p_crouch)
        {
            while (!Mathf.Approximately(m_cameraTransform.position.y, m_crouchPos.position.y))
            {
                m_cameraTransform.position = Vector3.MoveTowards(m_cameraTransform.position, m_crouchPos.position, m_speedCrouch);
                yield return new WaitForSeconds(0.001f);
            } 
        }
        else
        {
            while (!Mathf.Approximately(m_cameraTransform.position.y, m_standPos.position.y))
            {
                m_cameraTransform.position = Vector3.MoveTowards(m_cameraTransform.position, m_standPos.position, m_speedCrouch);
                yield return new WaitForSeconds(0.001f);
            } 
        }
        yield return 0;
    }
}
