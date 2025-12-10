using System;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Wolf : MonoBehaviour, ILife
{
    public float health = 10f;
    public NavMeshAgent agent;
    public Animator animator;
    public Transform target = null;
    [SerializeField] public LayerMask terrain_Layer;
    [SerializeField] public LayerMask prey_layer;

    public float patrol_range = 5f;

    //States
    private IState current_state;
    public WolfPatrol wolf_patrol;
    public WolfChase wolf_chase;
    public WolfAttack wolf_attack;

    public bool can_attack = true;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
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
        current_state.Tick();
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

        if (health <= 0)
        {
            die();
        }
    }

    public bool is_alive()
    {
        animator.SetTrigger("isDie");
        return health > 0;
    }

    public void die()
    {
        StartCoroutine(Death());
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(1.34f);
        Destroy(this.gameObject);
    }

}
