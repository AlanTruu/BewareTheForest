using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool hovering; // checks if the player is hovering an item or not in the slot

    private ItemSO currentItem;
    private int currentItemAmount; // Amount of the same item is in the the same slot (stackable items)

    private Image iconImage;
    private TextMeshProUGUI amountText;

    private void Awake()
    {
        // Both of these information are retreived from children of Slot GameObject 
        iconImage = transform.GetChild(0).GetComponent<Image>(); 
        amountText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    public ItemSO GetItem()
    {
        return currentItem;
    }

    public int GetAmount()
    {
        return currentItemAmount;
    }
    
    // Set an Item information with its amount
    public void SetItem(ItemSO item, int amount = 1)
    {
        currentItem = item;
        currentItemAmount = amount;

        UpdateSlot();
    }

    public void UpdateSlot()
    {
        if (currentItem != null)
        {
            // Update Icon image data and text of its amount
            iconImage.enabled = true;
            iconImage.sprite = currentItem.icon;
            amountText.text = currentItemAmount.ToString();
        }
        else
        {
            // Turn off icon image in the slot and empty the text
            iconImage.enabled = false;
            amountText.text = "";
        }
    }

    // Function: When the item is stackable, add certain amounts of the same item when the same item is picked up
    public int AddAmount(int amount)
    {
        currentItemAmount += amount;

        UpdateSlot();
        return currentItemAmount;
    }

    public int RemoveAmount(int amount)
    {
        currentItemAmount -= amount;

        if (currentItemAmount <= 0)
        {
            // Clear the slot if no item is left to deduct
            ClearSlot();
        }
        else
        {
            // Update the Slot with deducted Item Amount
            UpdateSlot();
        }

        return currentItemAmount;
    }

    // Delete the information about the current slot
    public void ClearSlot()
    {
        currentItem = null;
        currentItemAmount = 0;
        UpdateSlot();
    }

    // Function: Check if item exist in the slot
    public bool HasItem()
    {
        return currentItem != null;
    }

    // Hovering is true when mouse hovers over the slot
    public void OnPointerEnter(PointerEventData eventData)
    {
        hovering = true;
    }

    // Hovering is false when mouse does not hover the slot
    public void OnPointerExit(PointerEventData eventData)
    {
        hovering = false;
    }
}
