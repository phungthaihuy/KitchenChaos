using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerr : MonoBehaviour, IKitchenObjectParent
{
    public static Playerr Instance { get; private set; }

    //the? SerializeField
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private Transform kitchenObjectHoldPoint;

    //Event
    public event EventHandler OnPlayerPickedUp;
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    //khai bao bien
    private Vector3 lastInteraction;
    private bool isWalking;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("dang co nhieu hon 1 nguoi choi");
        }
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAlternate += GameInput_OnInteractAlternate;
    }

    private void GameInput_OnInteractAlternate(object sender, EventArgs e)
    {
        if (!KitchenGameManager.Instance.IsGamePlaying()) return;

        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);    
        }
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (!KitchenGameManager.Instance.IsGamePlaying()) return;

        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
        else
        {
            Debug.Log("asd");
        }
        //Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        //Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        //if (moveDir != Vector3.zero)
        //{
        //    lastInteraction = moveDir;
        //}

        //float interactDistance = 2f;
        //if (Physics.Raycast(transform.position, lastInteraction, out RaycastHit raycastHit, interactDistance, countersLayerMask))
        //{
        //    if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
        //    {
        //        clearCounter.Interact();
        //    }
        //}
    }

    
    // Update is called once per frame
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
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            lastInteraction = moveDir;
        }

        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteraction, out RaycastHit raycastHit, interactDistance, countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if (baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
                    //selectedCounter = clearCounter;

                    //OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs 
                    //{ 
                    //    selectedCounter = selectedCounter 
                    //});
                }
            }
            else
            {
                SetSelectedCounter(null);
                //selectedCounter = null;

                //OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
                //{
                //    selectedCounter = selectedCounter
                //});
            }
            // == ClearCounter clearCounter = raycastHit.transform.GetComponent<ClearCounter>;
            //if (clearCounter != null)
            //{

            //}
        }
        else // not hit anything
        {
            SetSelectedCounter(null);
            //selectedCounter = null;

            //OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
            //{
            //    selectedCounter = selectedCounter
            //});
        }
        //Debug.Log(selectedCounter);
    }
    private void HandleMovement()
    {
        float playerHeight = 2f;
        float playerRadius = 1f;
        float moveDistance = moveSpeed * Time.deltaTime;
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        //Va cham vat ly bang ham CapsuleCast
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if (!canMove)
        {
            // va cham vat ly, chi di duoc huong X
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized; //chi di chuyen huong X
            canMove = moveDirX.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                // khong di duoc huong X, chuyen qua huong Z
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized; //chi di chuyen huong Z
                canMove = moveDirZ.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
                if (canMove)
                {
                    moveDir = moveDirZ;
                }
                else
                {
                    //khong di duoc huong nao het.
                }
            }

        }
        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }
        //

        isWalking = moveDir != Vector3.zero;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * 10f);
    }

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }

    public Transform GetKitchenObjectFollower()
    {
        return kitchenObjectHoldPoint;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;

        if (this.kitchenObject != null)
        {
            OnPlayerPickedUp.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}