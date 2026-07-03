using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private float movementSpeed = 7f;
    [SerializeField] private PlayerInput playerInput;
    
    private bool isWalking;
    
    private void Update() {
        Vector2 inputVector = playerInput.GetMovementVectorNormalized();
        
        Vector3 movementDirection = new Vector3(inputVector.x, 0, inputVector.y);
        transform.position += movementDirection * (movementSpeed * Time.deltaTime);
        isWalking = movementDirection != Vector3.zero;
        
        float rotationSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, movementDirection, Time.deltaTime * rotationSpeed);
    }

    public bool IsWalking() {
        return isWalking;
    }
}