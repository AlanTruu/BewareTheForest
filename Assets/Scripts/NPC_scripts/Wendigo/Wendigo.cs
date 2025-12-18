using UnityEngine;
using UnityEngine.AI;

public class Wendigo : MonoBehaviour, ILife
{
    //References 
    public NavMeshAgent agent;
    public AudioSource audio_source;
    public Animator animator;
    private GameObject player;
    public LayerMask detectable_layer;

    //States
    private IState current_state;
    public WendigoChase wendigo_chase;
    public WendigoAttack wendigo_attack;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        audio_source = GetComponent<AudioSource>();
        animator = GetComponentInChildren<Animator>();
    }


    void Start()
    {
        player = SuperManager.player;
        wendigo_chase = new WendigoChase(this);
        wendigo_attack = new WendigoAttack(this);

        current_state = wendigo_chase;
    }

    // Update is called once per frame
    void Update()
    {
        current_state.Tick();

    }

    public void switch_state(IState state)
    {
        if (current_state == state) return;

        current_state.OnExit();
        current_state = state;
        current_state.OnEnter();
    }

}
