using UnityEngine;

public class SpruceTree : MonoBehaviour
{
    public float health = 100f;
    public GameObject treePrefab;
    public AudioSource treeAudio;
    public AudioClip chopClip;

    // Call this when the tree takes damage
    public void TakeDamage(float damage)
    {
        if (treeAudio != null) AudioSource.PlayClipAtPoint(chopClip, transform.position);
        health -= damage;
        if (health <= 0f)
        {
            ChopDown();
        }
    }

    private void ChopDown()
    {
        Destroy(gameObject);

        // play effects, animation, drop logs, etc.
        Instantiate(treePrefab, transform.position, Quaternion.identity);
    }
}
