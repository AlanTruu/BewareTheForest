using UnityEngine;

public class NewFlashlightScript : MonoBehaviour
{
    private Rigidbody rb;
    private bool isHeld = false;
    public Light light;
    
    void Start()
    {
        light = light.GetComponent<Light>();

        if (light != null)
            light.enabled = false;
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
        Debug.Log("Flashlight turned on!");
        // Replace this with action
        if (light != null)
            {
                light.enabled = !light.enabled; // toggle
                Debug.Log("Flashlight " + (light.enabled ? "ON" : "OFF"));
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
