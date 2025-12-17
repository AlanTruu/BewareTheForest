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

    void Start()
    {
        animator = GetComponent<Animator>();
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
                isEating = true;
                animator.SetTrigger("Eat");
                DoAction();
                Invoke(nameof(EatSound), .2f);
                Invoke(nameof(ResetEat), .76f);
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
