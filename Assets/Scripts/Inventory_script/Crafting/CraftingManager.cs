using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    public Inventory inventory; // Instance of Inventory class to use its functionality

    public bool Craft(CraftingRecipe recipe)
    {
        // Check ingredients and see if inventory has enough
        foreach (var ingredients in recipe.ingredients)
        {
            // Cannot be created if not enough item in the inventory
            if (inventory.GetTotalAmount(ingredients.item) < ingredients.amount)
            {
                return false;
            }
        }

        // Check the necessary ingredients and remove the same amount from the inventory
        foreach (var ingredients in recipe.ingredients)
        {
            inventory.RemoveItem(ingredients.item, ingredients.amount);
        }

        // Add crafted item to the inventory slot
        inventory.AddItem(recipe.outputItem, recipe.outputAmount);

        return true;
    }
}
