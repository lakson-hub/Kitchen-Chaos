using System;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private float movementSpeed = 7f;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private LayerMask countersLayerMask;
    
    private bool isWalking;
    private Vector3 lastInteractDirection;

    private void Start() {
        playerInput.OnInteractAction += PlayerInput_OnInteractAction;
    }

    private void PlayerInput_OnInteractAction(object sender, EventArgs e) {
        Vector2 inputVector = playerInput.GetMovementVectorNormalized();
        Vector3 movementDirection = new Vector3(inputVector.x, 0, inputVector.y);

        // We keep direction we are facing at all times!
        if (movementDirection != Vector3.zero) {
            lastInteractDirection = movementDirection;
        }

        // Checking what is in front of player
        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteractDirection, out RaycastHit raycastHit, interactDistance, countersLayerMask)) {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter)) {
                // Raycast detected object that has ClearCounter component!
                clearCounter.Interact();
            }
        }
    }

    private void Update() {
        HandleMovement();
        HandleInteractions();
    }

    private void HandleInteractions() {
        Vector2 inputVector = playerInput.GetMovementVectorNormalized();
        Vector3 movementDirection = new Vector3(inputVector.x, 0, inputVector.y);

        // We keep direction we are facing at all times!
        if (movementDirection != Vector3.zero) {
            lastInteractDirection = movementDirection;
        }

        // Checking what is in front of player
        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteractDirection, out RaycastHit raycastHit, interactDistance, countersLayerMask)) {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter)) {
                // Raycast detected object that has ClearCounter component!
            }
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
            canMove = !Physics.CapsuleCast(transform.position, playerTopPoint, playerRadius, movementDirectionX, movementDistance);

            if (canMove) {
                // We can move only on the X axis!
                movementDirection = movementDirectionX;
            } else {
                // We cannot move only on the X axis!
                
                // Attempt movement on the Z axis
                Vector3 movementDirectionZ = new Vector3(0, 0, movementDirection.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, playerTopPoint, playerRadius, movementDirectionZ, movementDistance);

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
}