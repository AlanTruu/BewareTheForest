using UnityEngine;

public class MeatFuncScript : MonoBehaviour
{
    private Rigidbody rb;
    private bool isHeld = false;
    private float healAmount = 10f;
    private Animator animator;
    private bool isEating;
    public AudioSource audioSource;
    public AudioClip audioClip;

    // Meat reference for Item info
    public ItemSO meat;
    private Inventory inventory; 

    void Start()
    {
        animator = GetComponent<Animator>();
        inventory = FindObjectOfType<Inventory>(); // Need this because script is not attached to scene object
    }

    void Update()
    {
        // Check if this object is a child of an object named "Hand"
        if (IsChildOfName(transform, "Hand"))
        {
            if (!isHeld)
            {
                rb = GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = false;   // disable physics
                    rb.useGravity = false;     // disable gravity
                }
                isHeld = true;
            }

            // Left mouse click triggers action
            if (Input.GetMouseButtonDown(0) && !isEating)
            {
                // Check if meat is left in the inventory
                if (inventory != null && meat != null && inventory.GetTotalAmount(meat) > 0)
                {
                    isEating = true;
                    animator.SetTrigger("Eat");
                    DoAction();
                    Invoke(nameof(EatSound), .2f);
                    Invoke(nameof(ResetEat), .76f);
                }
                else
                {
                    DestroyPrefab(); // Remove the meat_hand prefab from the player
                }
            }
        }
        else
        {
            isHeld = false;
        }
    }

    void DoAction()
    {
        Debug.Log("Meat Regained Health!");
        // Replace this with action
        GameObject playerController = GameObject.Find("FirstPersonController");
        if (playerController != null)
        {
            PlayerData playerData = playerController.GetComponent<PlayerData>();
            if (playerData != null)
            {
                if (playerData.Health <= 90)
                {
                    playerData.Health += healAmount;
                }
                else if (playerData.Health > 90 && playerData.Health < 100)
                {
                    playerData.Health = 100;
                }
            }
        }

        // Remove meat from inventory after eating
        if (inventory != null && meat != null)
        {
            inventory.RemoveItem(meat, 1);

            // Destroy meat immediately after eating the last piece
            if (inventory.GetTotalAmount(meat) <= 0)
            {
                Invoke(nameof(DestroyPrefab), 0.76f);
            }
        }
    }

    // Function: Destroy the meat prefab equipped on hand, and empty it
    void DestroyPrefab()
    {
        Destroy(gameObject);

        if (inventory != null)
        {
            inventory.EquipHandItem(); // Empty the hand equip item
        }
    }

    void EatSound()
    {
        if (audioSource != null) audioSource.PlayOneShot(audioClip);
    }
    void ResetEat()
    {
        isEating = false;
    }

    // Check recursively if this object is a child of a parent with the specified name
    private bool IsChildOfName(Transform child, string parentName)
    {
        while (child != null)
        {
            if (child.name == parentName)
                return true;
            child = child.parent;
        }
        return false;
    }
}
