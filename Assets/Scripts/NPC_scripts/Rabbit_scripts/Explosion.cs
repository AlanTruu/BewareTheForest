using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public AudioSource audio_source;
    public AudioClip explosion_sound;
    public float explosion_damage = 9999f;
    [SerializeField] public LayerMask detectable_layer;

    void Awake()
    {
        audio_source = GetComponent<AudioSource>();
    }


    void Start()
    {
        audio_source.PlayOneShot(explosion_sound);
        Collider[] hits = Physics.OverlapSphere(this.transform.position, 10f, detectable_layer);

        foreach (var hit in hits)
        {
            ILife life_component = hit.GetComponent<ILife>();

            if (life_component != null && hit.gameObject != this.gameObject)
            {
                Debug.Log("Damage sustained!");
                life_component.take_damage(explosion_damage);
            }
        }

        StartCoroutine(self_destruct());
    }


    IEnumerator self_destruct()
    {
        yield return new WaitForSeconds(.37f);
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
