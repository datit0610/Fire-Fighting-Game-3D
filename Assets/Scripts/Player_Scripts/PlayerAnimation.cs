using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private InputManager inputManager;
    private static int inputXHash = Animator.StringToHash("inputX");
    private static int inputYHash = Animator.StringToHash("inputY");
    private static int inputMagnitudeHash = Animator.StringToHash("inputMagnitude");
    private PlayerState playerState;
    private Vector3 currentBlendInput = Vector3.zero;

    public float blendSpeed = 4f;
    public Animator animator;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerState = GetComponent<PlayerState>();
    }

    private void Update()
    {
        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        bool isGrounded = playerState.IsGroundedState();
        bool isJumping = playerState.currentPlayerMovementState == PlayerMovementState.Jumping;
        bool isFalling = playerState.currentPlayerMovementState == PlayerMovementState.Falling;
        bool isSprinting = playerState.currentPlayerMovementState == PlayerMovementState.Sprinting && inputManager.movementInput.y > 0;

        Vector2 input = isSprinting ? inputManager.movementInput * 1.5f : inputManager.movementInput;

        currentBlendInput = Vector3.Lerp(currentBlendInput, input, blendSpeed * Time.deltaTime);

        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isJumping", isJumping);
        animator.SetBool("isFalling", isFalling);
        animator.SetFloat(inputXHash, currentBlendInput.x);
        animator.SetFloat(inputYHash, currentBlendInput.y);
        animator.SetFloat(inputMagnitudeHash, currentBlendInput.magnitude);
        if (isJumping)
        {
            AudioManager.instance.PlayFX("Jump");
        }
    }
}
