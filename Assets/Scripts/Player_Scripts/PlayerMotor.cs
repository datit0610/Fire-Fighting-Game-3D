using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private InputManager inputManager;
    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;
    private float verticalVelocity = 0;
    private float sprintSpeed;
    private float speed;
    private PlayerState playerState;
    private GameManager gameManager;
    //private Useable use;
    private Interactor baochay;
    private PickupClass_Mi _pickup;
    private ManagerSceneUnlocks _managerSceneUnlocks;
    
    public float runSpeed = 5f;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;
    public bool unlockMap;
    

    // Start is called before the first frame update
    void Start()
    {
        _pickup = GameObject.FindObjectOfType<PickupClass_Mi>();
        baochay = GameObject.FindObjectOfType<Interactor>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
        controller = GetComponent<CharacterController>();
        inputManager = GetComponent<InputManager>();
        playerState = GetComponent<PlayerState>();
        sprintSpeed = runSpeed * 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovementState();
        
    }
    private void FixedUpdate()
    {
        Jump();
    }
    public void ProcessMove(Vector2 input)
    {  
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        moveDirection.y = verticalVelocity;
        speed = playerState.currentPlayerMovementState == PlayerMovementState.Sprinting ? sprintSpeed : runSpeed;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
    }

    public void Jump()
    {
        bool isGrounded = playerState.IsGroundedState();
        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = 0;
        }

        verticalVelocity += gravity * Time.deltaTime;
        if (isGrounded && inputManager.jumpPressed)
        {
            verticalVelocity += Mathf.Sqrt(jumpHeight * -3f * gravity);
        }
        inputManager.jumpPressed = false;
    }

    public void UpdateMovementState()
    {
        bool isMovementInput = inputManager.movementInput != Vector2.zero;
        bool isMovingLaterally = IsMovingLaterally();
        bool isSprinting = inputManager.holdToSprint && isMovingLaterally && inputManager.movementInput.y > 0;
        bool isGrounded = controller.isGrounded;

        PlayerMovementState lateralState = isSprinting ? PlayerMovementState.Sprinting :
                                           isMovingLaterally || isMovementInput ? PlayerMovementState.Running : PlayerMovementState.Idling;

        playerState.SetPlayerMovementState(lateralState);

        if (!isGrounded && verticalVelocity > 0)
        {
            playerState.SetPlayerMovementState(PlayerMovementState.Jumping);
        }
        else if (!isGrounded && verticalVelocity < 0)
        {
            playerState.SetPlayerMovementState(PlayerMovementState.Falling);
        }
        
    }

    private bool IsMovingLaterally()
    {
        Vector3 lateralVelocity = new Vector3(moveDirection.x, 0f, moveDirection.z);
        return lateralVelocity.magnitude > 0.01f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CheckPoint")
        {
            Debug.Log("ok");
            if (_pickup.use == Useable.Khan && baochay.baochay==true)
            {
                unlockMap = true;
                gameManager.isFinish.isOn = true;
                AudioManager.instance.PlayFX("Winner");
                AudioManager.instance.StopMBG("Fire");
                AudioManager.instance.StopMBG("BaoChay");
                gameManager.winpanel.SetActive(true);
                gameManager.pause = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }

        
    }
}
