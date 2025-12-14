using UnityEngine;

public class NewHatchetScript : MonoBehaviour
{
    private Rigidbody rb;
    private bool isHeld = false;
    private Camera playerCamera;
    public float damage = 50f;
    public float range = 3f;

    void Start()
    {
        playerCamera = Camera.main;
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
            if (Input.GetMouseButtonDown(0))
            {
                DoAction();
            }
        }
        else
        {
            isHeld = false;
        }
    }

    void DoAction()
    {
        Debug.Log("Hatchet swung!");
        // Replace this with action
        Swing();
    }

    void Swing()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range))
        {
            SpruceTree tree = hit.collider.GetComponent<SpruceTree>();
            if (tree != null)
            {
                tree.TakeDamage(damage);
            }
        }

        // Optional: play swing animation, sound, etc.
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
