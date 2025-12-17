using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public AudioSource audio_source;
    public AudioClip explosion_sound;
    public float explosion_damage = 50f;
    [SerializeField] public LayerMask detectable_layer;
    [SerializeField] public ParticleSystem explosion_FX;

    void Awake()
    {
        audio_source = GetComponent<AudioSource>();
    }


    void Start()
    {
        audio_source.PlayOneShot(explosion_sound);
        Instantiate(explosion_FX, transform.position, Quaternion.identity, transform);

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

        Debug.Log("Self destructing...");
        StartCoroutine(self_destruct());
    }


    IEnumerator self_destruct()
    {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
