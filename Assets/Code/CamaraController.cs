using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class CamaraController : MonoBehaviour
{
    public float m_YawSpeed = 720.0f;
    public float m_PitchSpeed = 360.0f;
    public float m_MinPitch = 80.0f;
    public float m_MaxPitch = 80.0f;
    float m_Yaw = 0.0f;
    float m_Pitch = 0.0f;
    public Transform m_LookAt;
    public float m_CameraDistance = 8.0f;


    private void LateUpdate()
    {
        DeltaControl l_DeltaMouse = Mouse.current.delta;
        m_Yaw = m_Yaw + l_DeltaMouse.x.value * m_YawSpeed * Time.deltaTime;
        m_Pitch = m_Pitch + l_DeltaMouse.y.value * m_PitchSpeed * Time.deltaTime;

        m_Pitch = Mathf.Clamp(m_Pitch, m_MinPitch, m_MaxPitch);

        float l_YawRadians = m_Yaw * Mathf.Deg2Rad;
        float l_PitchRadians = m_Pitch * Mathf.Deg2Rad;
        Vector3 l_Direction = new Vector3(Mathf.Cos (l_YawRadians) * Mathf.Cos(l_PitchRadians), Mathf.Sin(l_PitchRadians), Mathf.Sin(l_YawRadians) * Mathf.Cos(l_PitchRadians));
        float l_CameraDistance = m_CameraDistance;
        Vector3 l_DesiredPosition = m_LookAt.position + l_Direction * l_CameraDistance;

        transform.position = l_DesiredPosition;
        transform.LookAt(m_LookAt.position);
    }

    
}
