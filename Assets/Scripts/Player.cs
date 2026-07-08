using System;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent {
    
    public static Player Instance { get; private set; }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs {
        public BaseCounter selectedCounter;
    }

    [SerializeField] private float movementSpeed = 7f;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private Transform kitchenObjectHoldPoint;
    
    private bool isWalking;
    private Vector3 lastInteractDirection;
    private BaseCounter baseCounter;
    private KitchenObject kitchenObject;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There is more than one Player instance!");
        }
        
        Instance = this;
    }

    private void Start() {
        playerInput.OnInteractAction += PlayerInput_OnInteractAction;
        playerInput.OnInteractAlternateAction += PlayerInput_OnInteractAlternateAction;
    }

    private void PlayerInput_OnInteractAlternateAction(object sender, EventArgs e) {
        if (baseCounter != null) {
            baseCounter.InteractAlternate(this);
        }
    }

    private void PlayerInput_OnInteractAction(object sender, EventArgs e) {
        if (baseCounter != null) {
            baseCounter.Interact(this);
        }
    }

    private void Update() {
        HandleMovement();
        HandleInteractions();
    }

    private void HandleInteractions() {
        Vector2 inputVector = playerInput.GetMovementVectorNormalized();
        Vector3 movementDirection = new Vector3(inputVector.x, 0, inputVector.y);

        // We keep direction Player is facing at all times!
        if (movementDirection != Vector3.zero) {
            lastInteractDirection = movementDirection;
        }

        // Checking what is in front of player
        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteractDirection, out RaycastHit raycastHit, interactDistance, countersLayerMask)) {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter)) {
                // Raycast detected object that has ClearCounter component!
                if (baseCounter != this.baseCounter) {
                    SetSelectedCounter(baseCounter);
                }
            } else {
                SetSelectedCounter(null);
            }
        } else {
            SetSelectedCounter(null);
        }
    }

    private void HandleMovement() {
        Vector2 inputVector = playerInput.GetMovementVectorNormalized();
        Vector3 movementDirection = new Vector3(inputVector.x, 0, inputVector.y);
        float movementDistance = movementSpeed * Time.deltaTime;
        
        // We check if there is something in front of the player!
        float playerRadius = .7f;
        float playerHeight = 2f;
        Vector3 playerTopPoint = transform.position + Vector3.up * playerHeight;
        bool canMove = !Physics.CapsuleCast(transform.position, playerTopPoint, playerRadius, movementDirection, movementDistance);

        if (!canMove) {
            // We cannot move towards movementDirection!
            
            // Attempt movement on the X axis
            Vector3 movementDirectionX = new Vector3(movementDirection.x, 0, 0).normalized;
            canMove = movementDirection.x != 0 && !Physics.CapsuleCast(transform.position, playerTopPoint, playerRadius, movementDirectionX, movementDistance);

            if (canMove) {
                // We can move only on the X axis!
                movementDirection = movementDirectionX;
            } else {
                // We cannot move only on the X axis!
                
                // Attempt movement on the Z axis
                Vector3 movementDirectionZ = new Vector3(0, 0, movementDirection.z).normalized;
                canMove = movementDirection.z != 0 && !Physics.CapsuleCast(transform.position, playerTopPoint, playerRadius, movementDirectionZ, movementDistance);

                if (canMove) {
                    // We can move only on the Z axis!
                    movementDirection = movementDirectionZ;
                } else {
                    // We cannot move in any direction!
                }
            }
        }
        
        if (canMove) {
            transform.position += movementDirection * movementDistance;   
        }
        isWalking = movementDirection != Vector3.zero;
        
        float rotationSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, movementDirection, Time.deltaTime * rotationSpeed);
    }

    public bool IsWalking() {
        return isWalking;
    }

    private void SetSelectedCounter(BaseCounter selectedCounter) {
        this.baseCounter = selectedCounter;
        
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs {
            selectedCounter = selectedCounter
        });
    }
    
    public Transform GetKitchenObjectFollowTransform() {
        return kitchenObjectHoldPoint; 
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }

    public void ClearKitchenObject() {
        kitchenObject = null;
    }

    public bool HasKitchenObject() {
        return kitchenObject != null;
    }
}