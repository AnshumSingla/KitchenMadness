using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{

    [SerializeField] private float movespeed = 10f;
    [SerializeField] private GameInput gameInput;
    // We didn't use public because that would allow all classes to change it's value and [serializedField] allows us to change this value in
    //  unity.
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private Transform kitchenObjectHoldPoint;
    private Vector3 lastInteractDir;
    private bool isWalking;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;


    //This is a property we rarely use them it is acting like a getter and setter function
    //static field means this belongs to this instance only
    public static Player Instance { get; private set; }
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }


    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractionAction;
        //This is the method to listen to the event
    }


    private void GameInput_OnInteractionAction(object sender, System.EventArgs e)
    {
        if (selectedCounter != null)
            selectedCounter.Interact(this);
    }


    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("There is more than one player instance");
        Instance = this;
    }


    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }


    public bool IsWalking()
    {
        return isWalking;
    }


    private void HandleInteractions()
    {
        Vector2 vel = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(vel.x, 0f, vel.y);

        if (moveDir != Vector3.zero)
            lastInteractDir = moveDir;

        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, countersLayerMask))
        {
            //RaycastAll is a function that returns an array of all the objects that the laser encounters or use layer mask for getting the
            //actual object you want to
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                //TryGetComponent is better than GetComponent function as it automatically handels the NULL case
                if (baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
                }
                else
                {
                    SetSelectedCounter(null);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }


    private void HandleMovement()
    {
        Vector2 vel = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(vel.x, 0f, vel.y);

        // Delta time is the time elapsed since last frame was rendered so it keeps in check that the speed is controlled by the refresh rate
        // of the screen and not the speed at which the computer can process.


        isWalking = moveDir != Vector3.zero;
        float rotatingspeed = 10f;


        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotatingspeed);
        // Used to rotate the 3d object in the direction it is moving. slerp is spherical interpolation and lerp is interpolation
        // slerp and lerp functions are used to smoothen the rotations of the 3d object.


        float moveDistance = movespeed * Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHeight = 2f;

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if (!canMove)
        {
            //Check if Diagonal movement is possible
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
            if (canMove)
                //Diagonal movement is possible because only x direction is free
                moveDir = moveDirX;

            else
            {
                //Diagonal movement is possible because only z direction is free
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
                if (canMove)
                    moveDir = moveDirZ;
            }
        }

        if (canMove)
            transform.position += moveDir * moveDistance;
    }


    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }


    public Transform GetKitchenObjectFollowTranform()
    {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }

}
