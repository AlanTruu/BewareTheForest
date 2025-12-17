using UnityEngine;

public class OldHatchetScript : MonoBehaviour
{
    private Rigidbody rb;
    private bool isHeld = false;
    private Camera playerCamera;
    private Animator animator;
    private bool isSwinging;
    public float damage = 50f;
    public float range = 3f;
    public AudioSource audioSource;
    public AudioClip audioClip;

    void Start()
    {
        playerCamera = Camera.main;
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
            if (Input.GetMouseButtonDown(0) && !isSwinging)
            {
                isSwinging = true;
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
        isSwinging = true;
        animator.SetTrigger("Swing");
        if (audioSource != null) audioSource.PlayOneShot(audioClip);
        Invoke(nameof(Swing), .2f);
        Invoke(nameof(ResetSwing), .5f);
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

    void ResetSwing()
    {
        isSwinging = false;
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
