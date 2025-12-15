using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Rabbit : MonoBehaviour, ILife
{
    public float health = 9f;

    //References
    public NavMeshAgent agent;
    public Animator animator;
    public Transform attacker = null;
    public AudioSource audio_source;
    public AudioClip death_sound;
    [SerializeField] public Transform explosion_prefab;
    [SerializeField] public LayerMask terrain_Layer; //For detecting round
    [SerializeField] public LayerMask detectable_layer;

    //Logic
    public float wander_range = 3f;
    public bool is_monster = false;

    //States
    public IState current_state;
    public RabbitWander rabbit_wander;
    public RabbitRun rabbit_run;

    void Awake()
    {

        //initialize component references and speed
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audio_source = GetComponent<AudioSource>();

        //Instantiate states
        rabbit_wander = new RabbitWander(this);
        rabbit_run = new RabbitRun(this);
        current_state = rabbit_wander;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        current_state.Tick();
    }

    public void switch_state(IState state)
    {
        //if switching to the current state, just return
        if (current_state == state)
        {
            return;
        }

        current_state.OnExit();
        current_state = state;
        current_state.OnEnter();
    }

    public void spawn_explosion()
    {
        Instantiate(explosion_prefab, transform.position, Quaternion.identity);
    }

    public void take_damage(float damage, Transform source = null)
    {
        health -= damage;
        rabbit_wander.attacked = true;
        attacker = source;

        if (damage >= 9999)
        {
            Destroy(this.gameObject);
        }
        else if (health <= 0)
        {
            die();
        }
    }

    public void die()
    {
        animator.SetTrigger("isDie");
        audio_source.Stop();
        audio_source.PlayOneShot(death_sound);
        StartCoroutine(death());
    }


    public bool is_alive()
    {
        return health > 0;
    }

    IEnumerator death()
    {
        yield return new WaitForSeconds(1.34f);
        Destroy(this.gameObject);
    }

    public IEnumerator delayed_death()
    {
        yield return null;
        Destroy(this.gameObject);
    }
}
