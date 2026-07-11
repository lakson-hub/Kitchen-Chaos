using System;
using UnityEngine;

public class PlatesCounter : BaseCounter {

    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;

    private const float SPAWN_PLATE_TIMER_MAX = 4f;
    private const int PLATES_SPAWNED_AMOUNT_MAX = 4;

    private float spawnPlateTimer;
    private int platesSpawnedAmount;

    private void Update() {
        spawnPlateTimer += Time.deltaTime;

        if (spawnPlateTimer > SPAWN_PLATE_TIMER_MAX) {
            spawnPlateTimer = 0f;

            if (platesSpawnedAmount < PLATES_SPAWNED_AMOUNT_MAX) {
                platesSpawnedAmount++;
                
                OnPlateSpawned?.Invoke(this,EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player) {
        if (!player.HasKitchenObject()) {
            // Player is empty handed!
            if (platesSpawnedAmount > 0) {
                // There is at least one plate on the counter
                platesSpawnedAmount--;

                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                OnPlateRemoved?.Invoke(this,EventArgs.Empty);
            }
        }
    }
}