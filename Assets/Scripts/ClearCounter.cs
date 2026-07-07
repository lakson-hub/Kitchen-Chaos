using UnityEngine;

public class ClearCounter : BaseCounter {

    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    
    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            // There is no KitchenObject here!
            if (player.HasKitchenObject()) {
                // Player is carrying KitchenObject!
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        } else {
            // There is a KitchenObject here!
        }
    }
}