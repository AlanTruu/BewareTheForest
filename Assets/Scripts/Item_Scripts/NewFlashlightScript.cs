using UnityEngine;

public class NewFlashlightScript : MonoBehaviour
{
    private Rigidbody rb;
    private bool isHeld = false;
    public Light flashLight;
    
    void Start()
    {
        flashLight = flashLight.GetComponent<Light>();

        if (flashLight != null)
            flashLight.enabled = false;
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
        Debug.Log("FlashflashLight turned on!");
        // Replace this with action
        if (flashLight != null)
            {
                flashLight.enabled = !flashLight.enabled; // toggle
                Debug.Log("FlashflashLight " + (flashLight.enabled ? "ON" : "OFF"));
            }
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
