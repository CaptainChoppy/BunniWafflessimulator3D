using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Transform PlayerBody;
    public Transform Camera;
    public CharacterController Controller;

    public InGameGUIManager GUIManager;

    public float Speed = 1.4f;
    public float JumpForce = 3.75f;
    public float Gravity = -9.80665f;
    public float FrictionMultiplier = 1.0f;
    public float MouseSensitivity = 25.0f;

    private float CameraxRotation;

    private float Horizontal;
    private float Vertical;
    private bool JumpAction = false;

    private bool IsGrounded => Controller.isGrounded;

    public bool FreezeInputs = false;

    private Vector3 Velocity;

    public Transform StartPosition;

    public bool MobileMode = false;

    public bool Up = false;
    public bool Down = false;
    public bool Left = false;
    public bool Right = false;

    private void Start()
    {
        Application.targetFrameRate = 45;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        MouseSensitivity = Options.MouseSensitivity;

        GetInputs();
        MovePlayer();
        MoveCamera();
    }

    public void GetInputs()
    {
        if(MobileMode == false)
        {
            Horizontal = 0;
            Vertical = 0;
        }
        else
        {
            if(Up == true)
            {
                Vertical = 1;
            }
            else if (Down == true)
            {
                Vertical = -1;
            }
            else
            {
                Vertical = 0;
            }

            if (Right == true)
            {
                Horizontal = -1;
            }
            else if (Left == true)
            {
                Horizontal = 1;
            }
            else
            {
                Horizontal = 0;
            }
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            if(GUIManager == null)
            {
                QuitGame();
            }
            else
            {
                GUIManager.SetMenuActive(true);
            }
        }

        if(FreezeInputs == true)
        {
            return;
        }

        if(MobileMode == false)
        {
            Horizontal = Input.GetAxis("Horizontal");
            Vertical = Input.GetAxis("Vertical");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Jump()
    {
        if(IsGrounded == false)
        {
            return;
        }

        JumpAction = true;
    }

    public void MoblieUpPress()
    {
        Up = true;
    }
    public void MoblieDownPress()
    {
        Down = true;
    }
    public void MoblieLeftPress()
    {
        Left = true;
    }
    public void MoblieRightPress()
    {
        Right = true;
    }

    public void MoblieUpRelease()
    {
        Up = false;
    }
    public void MoblieDownRelease()
    {
        Down = false;
    }
    public void MoblieLeftRelease()
    {
        Left = false;
    }
    public void MoblieRightRelease()
    {
        Right = false;
    }

    public void MoveCamera()
    {
        float MouseX = 0.0f;
        float MouseY = 0.0f;

        if (FreezeInputs == false)
        {
            MouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
            MouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;
        }

        CameraxRotation -= MouseY;
        CameraxRotation = Mathf.Clamp(CameraxRotation, -85.0f, 85.0f);

        Camera.localRotation = Quaternion.Euler(CameraxRotation, 0.0f, 0.0f);
        PlayerBody.Rotate(Vector3.up * MouseX);
    }

    public void MovePlayer()
    {
        if(GUIManager.InMenu == true)
        {
            return;
        }

        float verticalspeed = Velocity.y;

        Velocity = Vector3.ClampMagnitude(PlayerBody.right * Horizontal + PlayerBody.forward * Vertical, 1.0f) * Speed;

        Velocity.y = -0.5f;

        if (IsGrounded == false)
        {
            Velocity.y = verticalspeed + (Gravity * Time.deltaTime);
        }

        if(JumpAction == true)
        {
            Velocity += Vector3.up * JumpForce;

            JumpAction = false;
        }

        Controller.Move(Velocity * Time.deltaTime);
    }
}
