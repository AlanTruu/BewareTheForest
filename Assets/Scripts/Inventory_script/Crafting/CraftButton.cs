using UnityEngine;

public class CraftButton : MonoBehaviour
{
    public CraftingRecipe recipe;
    public CraftingManager craftingManager;

    public void CraftThis()
    {
        craftingManager.Craft(recipe);
    }
}
