using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CraftButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public CraftingRecipe recipe; // Specific recipe that has important ingredients to craft
    public CraftingManager craftingManager; // Has functionality to craft 

    // Item Recipe description variables
    public GameObject itemDescriptionParent;
    public Image itemDescriptionIcon;
    public TextMeshProUGUI itemDescriptionNameText;
    public TextMeshProUGUI itemRecipeDetailText;

    public void CraftThis()
    {
        craftingManager.Craft(recipe);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Check if recipe exists and there is prefab to craft
        if (recipe != null && recipe.outputItem != null)
        {
            // Enable Item Description/Recipe
            itemDescriptionParent.SetActive(true);
            itemDescriptionIcon.sprite = recipe.outputItem.icon;
            itemDescriptionNameText.text = recipe.outputItem.itemName;
            itemRecipeDetailText.text = recipe.outputItem.recipe;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemDescriptionParent.SetActive(false);
    }
}
