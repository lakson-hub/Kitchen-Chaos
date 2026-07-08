using UnityEngine;

public class CuttingCounter : BaseCounter {

    [SerializeField] private KitchenObjectSO cutKitchenObjectSO;
    
    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            // There is no KitchenObject here!
            if (player.HasKitchenObject()) {
                // Player is carrying KitchenObject!
                player.GetKitchenObject().SetKitchenObjectParent(this);
            } else {
                // Player has nothing!
            }
        } else {
            // There is a KitchenObject here!
            if (player.HasKitchenObject()) {
                // Player is carrying something
            } else {
                // Player is not carrying anything!
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player) {
        if (HasKitchenObject()) {
            // There is KitchenObject on cutting counter
            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(cutKitchenObjectSO, this);
        }
    }
}