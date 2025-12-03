using System;
using UnityEngine;
using UnityEngine.AI;

public class Wolf : MonoBehaviour
{
    private StateMachine machine;
    [SerializeField] public Transform player_Character;
    NavMeshAgent agent;
    Animator animator;
    [SerializeField] public LayerMask terrain_Layer;

    public float patrol_range = 5f;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        machine = new StateMachine();

        //Instantiate States
        var wolf_patrol = new WolfPatrol(this, agent, animator);
        var wolf_chase = new WolfChase(this, agent, animator);
        //Need transitions for:
        //Patrol <-> Chasing
        //Chasing <-> Attacking

        machine.SetState(wolf_patrol);

        //Idle State needs animator
        //Patrol needs this, navmesh, animator
        //Attack needs this, navmesh, animator
        add_transition(wolf_patrol, wolf_chase, detect_player);
        add_transition(wolf_chase, wolf_patrol, () => !detect_player());

        //need lambda to check for distance between player and this
        bool detect_player() => Vector3.Distance(transform.position, player_Character.position) < 5f;
    }
    void Start()
    {

        //wolf begins its life patrolling
        if (animator != null)
        {
            animator.SetBool("isPatrolling", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        machine.Tick();
    }

    void add_transition(IState from, IState to, Func<bool> condition)
    {
        machine.AddTransition(from, to, condition);
    }

    
    
}
