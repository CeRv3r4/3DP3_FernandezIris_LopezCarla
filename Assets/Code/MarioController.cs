using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class MarioController : MonoBehaviour
{
    CharacterController m_CharacterController;
    public Camera m_Camera;
    Animator m_Animator;
    public float m_WalkSpeed = 4.0f;
    public float m_RunSpeed = 8.0f;
    public float m_RotationLerpPct = 10.0f;
    public float m_JumpSpeed = 0.0f;
    float m_VerticalSpeed = 0.0f;

    [Header("Input")]
    public Key m_RightKeyControl = Key.D;
    public Key m_LeftKeyControl = Key.A;
    public Key m_ForwardKeyControl = Key.W;
    public Key m_BackwardKeyControl = Key.S;
    public Key m_RunKeyControl = Key.LeftShift;
    public Key m_JumpKeyControl = Key.Space;



    private void Awake()
    {
        m_CharacterController = GetComponent<CharacterController>();
        m_Animator = GetComponent<Animator>();
    }

    void Start()
    {

    }

    void Update()
    {
        Vector3 l_Right = m_Camera.transform.right;
        Vector3 l_Forward = m_Camera.transform.forward;
        l_Right.y = 0.0f;
        l_Forward.y = 0.0f;

        l_Right.Normalize();
        l_Forward.Normalize();

        Vector3 l_Direction = Vector3.zero;
        
        if (Keyboard.current[m_RightKeyControl].isPressed)
        {
            l_Direction = l_Right;

        }
        else if (Keyboard.current[m_LeftKeyControl].isPressed)
        {
            l_Direction = -l_Right;
        }


        if (Keyboard.current[m_ForwardKeyControl].isPressed)
        {
            l_Direction += l_Forward;
        }
        else if (Keyboard.current[m_BackwardKeyControl].isPressed)
        {
            l_Direction -= l_Forward;
        }


        bool l_IsMoving = l_Direction.sqrMagnitude > 0.5f;
        float l_AnimatorSpeed = 0.0f;
        float l_Speed = m_WalkSpeed;
        l_Direction.Normalize();
        

        if (l_IsMoving)
        {
            if (Keyboard.current[m_RunKeyControl].isPressed)
            {
                l_Speed = m_RunSpeed;
                l_AnimatorSpeed = 1.0f;

            }
            else
            {
                l_AnimatorSpeed = 0.3f;

            }

            Quaternion l_DesiredRotation = Quaternion.LookRotation(l_Direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, l_DesiredRotation, m_RotationLerpPct * Time.deltaTime);
        }

        if (Keyboard.current[m_JumpKeyControl].wasPressedThisFrame)
        {
            m_VerticalSpeed = m_JumpSpeed;

        }

        m_Animator.SetFloat("Speed", l_AnimatorSpeed);
        l_Direction *= l_Speed * Time.deltaTime;


        m_VerticalSpeed += Physics.gravity.y * Time.deltaTime;
        CollisionFlags l_CollisionFlags = m_CharacterController.Move(l_Direction);

        if(((l_CollisionFlags & CollisionFlags.Below) != 0) || (l_CollisionFlags & CollisionFlags.Above) != 0  && m_VerticalSpeed == 0.0f){
            m_VerticalSpeed = 0.0f;
        }

        m_CharacterController.Move(l_Direction);
    }


}
