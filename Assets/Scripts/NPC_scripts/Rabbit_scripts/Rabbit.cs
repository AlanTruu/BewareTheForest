using UnityEngine;
using UnityEngine.AI;

public class Rabbit : MonoBehaviour, ILife
{
    public float health = 9f;
    public NavMeshAgent agent;
    public Animator animator;
    [SerializeField] public LayerMask terrain_Layer;
    public float wander_range = 2f;
    public bool attacked = false;



    //States
    public IState current_state;
    public RabbitWander rabbit_wander;



    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        rabbit_wander = new RabbitWander(this);
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

    public void take_damage(float damage, string source = null)
    {
        if (!attacked)
        {
            attacked = true;
        }

        health -= damage;

        if (health <= 0)
        {
            die();
        }
    }

    public void die()
    {
        Destroy(this.gameObject);
    }
}
