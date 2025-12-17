using UnityEngine;

public class Base : MonoBehaviour
{
    public BabyManager kidManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Child"))
        {
            if (kidManager != null)
            {
                kidManager.KidReachedBase();
            }

            Destroy(other.gameObject);
        }
    }
}
