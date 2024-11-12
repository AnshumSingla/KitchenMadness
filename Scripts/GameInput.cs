using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{

    public event EventHandler OnInteractAction;
    //EventHandler is a delegate type we can also create our own delegates
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Interact.performed += Interact_performed;
        //performed is a delegate
        //We don't use paranthesis on the function since we don't want to call it we just simply want to pass the function as the reference 
    }
    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 vel = playerInputActions.Player.Move.ReadValue<Vector2>();

        vel = vel.normalized;
        return vel;
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
        //The above line can also be written as
        // if (OnInteractAction != null)
        //     OnInteractAction(this, EventArgs.Empty);
        //We can send some arguments with the events but in this case we don't need it so we can send empty
    }
}
