using UnityEngine;

public class CraftButton : MonoBehaviour
{
    public CraftingRecipe recipe; // Specific recipe that has important ingredients to craft
    public CraftingManager craftingManager; // Has functionality to craft 

    public void CraftThis()
    {
        craftingManager.Craft(recipe);
    }
}
