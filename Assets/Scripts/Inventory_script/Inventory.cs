using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    // Temporary Item to check functionality (will remove later)
    public ItemSO axeItem;
    public ItemSO meatItem;
    
    public GameObject hotbarObject;
    public GameObject inventorySlotParent;

    // Dragging items variables
    public Image dragIcon;
    private Slot draggedSlot = null;
    private bool isDragging = false;

    private List<Slot> inventorySlots = new List<Slot>(); // List of inventory slots
    private List<Slot> hotbarSlots = new List<Slot>(); // List of hotbar slots
    private List<Slot> combinedSlots = new List<Slot>(); // List of inventory + hotbar slots

    private void Awake()
    {
        // Find all Slots for each gameObjects and put them into respective list
        inventorySlots.AddRange(inventorySlotParent.GetComponentsInChildren<Slot>());
        hotbarSlots.AddRange(hotbarObject.GetComponentsInChildren<Slot>());

        // Combine the two list into one
        combinedSlots.AddRange(inventorySlots);
        combinedSlots.AddRange(hotbarSlots);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            AddItem(axeItem, 1);

        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            AddItem(meatItem, 3);
        }

        StartDrag();
        HandleDragIconPosition();
        EndDrag();
    }

    public void AddItem(ItemSO item, int amount)
    {
        int remaining = amount; // Remaining will be used for calculating stackable items

        // Check each slots
        foreach(Slot slot in combinedSlots)
        {
            // If the slot has an item and its item is the same item to add, stack them if possible
            if (slot.HasItem() && slot.GetItem() == item)
            {
                // Gather original information before updating
                int currentAmount = slot.GetAmount();
                int maxStack = item.maxStackSize; 

                // If there is room left to stack item, stack them and reduce the remaining stackable amount
                if (currentAmount < maxStack)
                {
                    int newRemaining = maxStack - currentAmount; // How many more items can fit in the slot

                    // Check if amount to add or newRemaining is smaller because it cannot exceed the maxStack size.
                    // Remaining item will be added to the next slot
                    int amountToAdd = Mathf.Min(newRemaining, remaining);

                    slot.SetItem(item, amountToAdd + currentAmount);
                    remaining -= amountToAdd;


                    if (remaining <= 0)
                    {
                        return;
                    }
                }
            }
        }

        // Iterate through each Slot and add items on empty slots up to maximum Stack of that item
        foreach (Slot slot in combinedSlots)
        {
            // If the slot does not have an item, add a new item
            if (!slot.HasItem())
            {
                int amountToAdd = Mathf.Min(item.maxStackSize, remaining);
                slot.SetItem(item, amountToAdd);
                remaining -= amountToAdd;

                if (remaining <= 0)
                {
                    return;
                }
            }
        }

        // If the inventory is full
        if (remaining > 0)
        {
            Debug.Log("Inventory is full, cannot add " + remaining + " of " + item.itemName);
        }
    }

    private void StartDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Slot hoveringSlot = GetHoverredSlot(); // Get hoverred slot


            if (hoveringSlot != null && hoveringSlot.HasItem())
            {
                // Update variables 
                draggedSlot = hoveringSlot;
                isDragging = true;

                // Enable image of dragged item
                dragIcon.sprite = hoveringSlot.GetItem().icon;
                dragIcon.color = new Color(1, 1, 1, 0.5f);
                dragIcon.enabled = true;
            }
        }
    }

    private void EndDrag()
    {
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            Slot hoveringSlot = GetHoverredSlot();

            if (hoveringSlot != null)
            {
                DropIcon(draggedSlot, hoveringSlot);

                dragIcon.enabled = false; // Original slot will no longer have icon
                draggedSlot = null; // Original slot is empty
                isDragging = false;
            }
        }
    }

    // Function: Handles dragging items in the slot
    private void DropIcon(Slot start, Slot end)
    {
        // Return if icon is dropped in the same slot
        if (start == end)
        {
            return;
        }

        // Stacking the same item in the end Slot
        if (end.HasItem() && (start.GetItem() == end.GetItem()))
        {
            // Get the max stack size of the item and calculate remaining
            int maxSize = end.GetItem().maxStackSize;
            int remaining = maxSize - end.GetAmount();

            
            if (remaining > 0)
            {
                int move = Mathf.Min(remaining, start.GetAmount());

                end.SetItem(end.GetItem(), move + end.GetAmount()); // End Slot is getting added items
                start.SetItem(start.GetItem(), start.GetAmount() - move); // Start slot is losing items

                // if the starting slot is now empty, clear the slot
                if (start.GetAmount() <= 0)
                {
                    start.ClearSlot();
                }
            }
        }

        // Dropping to different Item Slot (Swap locations)  
        if (end.HasItem())
        {
            ItemSO temp = end.GetItem();
            int tempAmount = end.GetAmount();

            // Swap locations
            end.SetItem(start.GetItem(), start.GetAmount());
            start.SetItem(temp, tempAmount);

            return;
        }

        // If Dropping to empty slot, clear the original slot
        end.SetItem(start.GetItem(), start.GetAmount());
        start.ClearSlot();
    }

    // Function: Return slot that is getting hoverred
    private Slot GetHoverredSlot()
    {
        foreach(Slot slot in combinedSlots)
        {
            if (slot.hovering)
            {
                return slot;
            }
        }

        return null;
    }

    // Function: Handles icon of the item moving slots
    private void HandleDragIconPosition()
    {
        if (isDragging)
        {
            dragIcon.transform.position = Input.mousePosition;
        }
    }
}
