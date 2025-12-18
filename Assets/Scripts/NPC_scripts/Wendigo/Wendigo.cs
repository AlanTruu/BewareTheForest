using System.Collections;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.AI;

public class Wendigo : MonoBehaviour, ILife
{
    //References 
    public NavMeshAgent agent;
    public Animator animator;
    private GameObject player;
    public LayerMask detectable_layer;
    public AudioSource audio_source;
    public AudioClip screech;
    public AudioClip punch;

    //States
    private IState current_state;
    public WendigoChase wendigo_chase;
    public WendigoAttack wendigo_attack;

    //Logic
    public float attack_cd = 2f;
    public float attack_counter = 0f;
    public float attack_radius = 1f;
    public float attack_range = 6f;
    public float attack_enter_range = 5f;
    public float attack_exit_range = 6.5f; // MUST be larger

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

    public IEnumerator Punch_SFX()
    {
        yield return new WaitForSeconds(0.2f);
        audio_source.Stop();
        audio_source.PlayOneShot(punch);
    }


    public IEnumerator attack()
    {
        animator.SetTrigger("attack");
        yield return new WaitForSeconds(0.7f);
        audio_source.PlayOneShot(punch);

        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;
        RaycastHit[] hits = Physics.SphereCastAll(origin, attack_radius, direction, attack_range, detectable_layer);

        foreach (RaycastHit hit in hits)
        {
            Debug.DrawLine(origin, hit.point, Color.yellow);
            Debug.DrawRay(hit.point, hit.normal, Color.cyan);

            ILife life = hit.collider.GetComponent<ILife>();

            if (life != null)
            {
                life.take_damage(100f, transform);
            }
        }

    }



}
