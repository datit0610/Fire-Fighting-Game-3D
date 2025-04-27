using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour, PlayerInput.IOnFootActions
{
    public bool holdToSprint = false;
    public bool sprintToggleOn {  get; private set; }
    public PlayerInput playerInput {  get; private set; }
    public bool jumpPressed { get; set; }
    public Vector2 movementInput;

    private PlayerInput.OnFootActions onFoot;
    private PlayerMotor motor;
    private Vector2 lookInput;


    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        motor = GetComponent<PlayerMotor>();

        //onFoot.Jump.performed += ctx => motor.Jump();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        motor.ProcessMove(movementInput);
    }
    private void OnEnable()
    {
        onFoot.Enable();
        onFoot.SetCallbacks(this);
    }

    private void OnDisable()
    {
        onFoot.Disable();
        onFoot.RemoveCallbacks(this);
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        
        if (movementInput.y > 0)
        {
            AudioManager.instance.PlayMBG("Run");
        }
        else if (movementInput.y < 0)
        {
            AudioManager.instance.PlayMBG("Walk");
        }else if (movementInput.x > 0)
        {
            AudioManager.instance.PlayMBG("Walk");
        }else if (movementInput.x < 0)
        {
            AudioManager.instance.PlayMBG("Walk");
        }
        
        else if (movementInput.y == 0 || movementInput.x == 0)
        {
            AudioManager.instance.StopMBG("Walk");
            AudioManager.instance.StopMBG("Run");
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            
            jumpPressed = true;
        } 
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            holdToSprint = true;
        }
        else if (context.canceled)
        {
            holdToSprint = false;
        }
    }
}
