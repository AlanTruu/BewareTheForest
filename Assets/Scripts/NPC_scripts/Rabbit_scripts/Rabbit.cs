using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Rabbit : MonoBehaviour, ILife
{
    public float health = 9f;
    public NavMeshAgent agent;
    public Animator animator;
    public float wander_range = 3f;
    public Transform attacker = null;
    [SerializeField] public LayerMask terrain_Layer;



    //States
    public IState current_state;
    public RabbitWander rabbit_wander;
    public RabbitRun rabbit_run;

    void Awake()
    {

        //initialize component references and speed
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

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

    public void take_damage(float damage, Transform source = null)
    {
        health -= damage;
        rabbit_wander.attacked = true;
        attacker = source;

        if (health <= 0)
        {
            die();
        }
    }

    public void die()
    {
        animator.SetTrigger("isDie");
        StartCoroutine(Death());
    }

    public bool is_alive()
    {
        return health > 0;
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(1.34f);
        Destroy(this.gameObject);
    }
}
