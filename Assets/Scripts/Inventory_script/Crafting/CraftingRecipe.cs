using UnityEngine;

[CreateAssetMenu(fileName = "CraftingRecipe", menuName = "Crafting/Recipe")]
public class CraftingRecipe : ScriptableObject
{
    public ItemSO outputItem;
    public int outputAmount = 1;

    // System Serializable = can be seen/edited in the Inspector, DO NOT MODIFY
    [System.Serializable]
    public class Ingredient
    {
        public ItemSO item; 
        public int amount;
    }
     
    // Necessary ingredients to craft this item
    public Ingredient[] ingredients;
}
