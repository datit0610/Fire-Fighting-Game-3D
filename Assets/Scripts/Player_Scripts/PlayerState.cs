using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerMovementState
{
    Idling = 0,
    Walking = 1,
    Running = 2,
    Sprinting = 3,
    Jumping = 4,
    Falling = 5,
    Strafing = 6,
}
public class PlayerState : MonoBehaviour
{
    [field: SerializeField] public PlayerMovementState currentPlayerMovementState { get; private set; } = PlayerMovementState.Idling;

    public void SetPlayerMovementState(PlayerMovementState playerMovementState)
    {
        currentPlayerMovementState = playerMovementState;
    }
    
    public bool IsGroundedState()
    {
        return currentPlayerMovementState == PlayerMovementState.Idling ||
               currentPlayerMovementState == PlayerMovementState.Walking ||
               currentPlayerMovementState == PlayerMovementState.Running ||
               currentPlayerMovementState == PlayerMovementState.Sprinting;
    }
}
