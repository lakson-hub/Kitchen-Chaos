using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryManager : MonoBehaviour {
    
    public static DeliveryManager Instance { get; private set; }
    
    [SerializeField] private RecipeListSO recipeListSO;

    private const float SPAWN_RECIPE_TIMER_MAX = 4f;
    private const int WAITING_RECIPES_MAX = 4;

    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;

    private void Awake() {
        Instance = this;
        
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update() {
        spawnRecipeTimer -= Time.deltaTime;

        if (spawnRecipeTimer <= 0f) {
            spawnRecipeTimer = SPAWN_RECIPE_TIMER_MAX;

            if (waitingRecipeSOList.Count < WAITING_RECIPES_MAX) {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[Random.Range(0, recipeListSO.recipeSOList.Count)];
                Debug.Log(waitingRecipeSO.recipeName);
                waitingRecipeSOList.Add(waitingRecipeSO);   
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject) {
        for (int i = 0; i < waitingRecipeSOList.Count; i++) {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            if (waitingRecipeSO.KitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count) {
                // Recipe and Plate have same amount of ingredients
                bool plateContentsMatchesRecipe = true;
                
                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.KitchenObjectSOList) {
                    // Cycling through all ingredients in the Recipe!
                    bool ingredientFound = false;
                    
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList()) {
                        // Cycling through all ingredients in the Plate!
                        if (plateKitchenObjectSO == recipeKitchenObjectSO) {
                            // Ingredient matches
                            ingredientFound = true;
                            break;
                        }
                    }

                    if (!ingredientFound) {
                        // This Recipe ingredient was not found on the Plate!
                        plateContentsMatchesRecipe = false;
                    }
                }

                if (plateContentsMatchesRecipe) {
                    // Player delivered the correct recipe!
                    Debug.Log("Player delivered the correct recipe!");
                    waitingRecipeSOList.RemoveAt(i);
                    return;
                }
            }
        }
        
        // No matches found, player did not deliver a correct recipe!
        Debug.Log("Player did not deliver a correct recipe!");
    }
}