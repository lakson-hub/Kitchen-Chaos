using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private float movementSpeed = 7f;
    [SerializeField] private PlayerInput playerInput;
    
    private bool isWalking;
    
    private void Update() {
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