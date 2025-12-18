using UnityEngine;

public class WendigoHitBox : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        ILife hit = other.GetComponent<ILife>();
        if (hit != null)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Hit?");
                hit.take_damage(100, this.transform);
            }
        }
    }
}
