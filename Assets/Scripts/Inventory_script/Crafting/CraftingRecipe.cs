using UnityEngine;

[CreateAssetMenu(fileName = "CraftingRecipe", menuName = "Crafting/Recipe")]
public class CraftingRecipe : ScriptableObject
{
    public ItemSO outputItem;
    public int outputAmount = 1;

    [System.Serializable]
    public struct Ingredient
    {
        public ItemSO item;
        public int amount;
    }
     
    // Necessary ingredients to craft this item
    public Ingredient[] ingredients;
}
