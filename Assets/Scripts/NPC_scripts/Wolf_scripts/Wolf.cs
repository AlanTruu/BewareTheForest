using System;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using Unity.VisualScripting;

public class Wolf : MonoBehaviour, ILife
{
    public float health = 10f;

    //References
    public NavMeshAgent agent;
    public Animator animator;
    public Transform target = null;
    public AudioSource audio_source;
    public AudioClip howl;
    public AudioClip death_sound;
    public AudioClip bite_sound;

    //LayerMasks needed to detect ground and detectables (ILife constructs)
    [SerializeField] public LayerMask terrain_Layer;
    [SerializeField] public LayerMask prey_layer;



    //States
    private IState current_state;
    public WolfPatrol wolf_patrol;
    public WolfChase wolf_chase;
    public WolfAttack wolf_attack;

    //logic
    public float patrol_range = 5f;
    public bool can_attack = true;
    private bool is_dead = false;
    public float damage = 3f;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audio_source = GetComponent<AudioSource>();
        wolf_patrol = new WolfPatrol(this, agent, animator);
        wolf_chase = new WolfChase(this, agent, animator);
        wolf_attack = new WolfAttack(this, agent, animator);
    }
    void Start()
    {

        //wolf begins its life patrolling
        if (animator != null)
        {
            // animator.SetBool("isPatrolling", true);
            animator.SetInteger("state", 1);
        }

        current_state = wolf_patrol;
    }

    // Update is called once per frame
    void Update()
    {
        if (current_state != null)
        {
            current_state.Tick();
        }


    }

    public void switch_state(IState state)
    {
        current_state.OnExit();
        current_state = state;
        current_state.OnEnter();
    }

    public void reset_can_attack()
    {
        can_attack = true;
    }

    public void call_reset_attack(float cooldown)
    {
        Invoke("reset_can_attack", cooldown);
    }

    public void take_damage(float damage, Transform source = null)
    {
        health -= damage;

        if (health <= 0 && !is_dead)
        {
            is_dead = true;
            die();
        }
    }

    public bool is_alive()
    {

        return health > 0;
    }

    public void die()
    {
        agent.ResetPath();
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        agent.updatePosition = false;
        agent.updateRotation = false;

        current_state = null;

        animator.SetTrigger("IsDIe");
        animator.SetFloat("speed", 0);

        audio_source.Stop();
        audio_source.PlayOneShot(death_sound);
        StartCoroutine(Death());
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(1.34f);
        Destroy(this.gameObject);
    }

}
