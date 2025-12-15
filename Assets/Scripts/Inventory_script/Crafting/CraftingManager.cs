using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    public Inventory inventory;

    public bool Craft(CraftingRecipe recipe)
    {
        // Check ingredients
        foreach (var ing in recipe.ingredients)
        {
            // Cannot be created if not enough item in the inventory
            if (inventory.GetTotalAmount(ing.item) < ing.amount)
            {
                Debug.Log("Not enough " + ing.item.itemName);
                return false;
            }
        }

        // Remove ingredients
        foreach (var ing in recipe.ingredients)
        {
            inventory.RemoveItem(ing.item, ing.amount);
        }

        // Add crafted item
        inventory.AddItem(recipe.outputItem, recipe.outputAmount);

        Debug.Log("Crafted " + recipe.outputItem.itemName);
        return true;
    }
}
