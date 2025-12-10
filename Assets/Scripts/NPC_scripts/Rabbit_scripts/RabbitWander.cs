using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class RabbitWander : IState
{

    //References
    private Rabbit _rabbit;
    private NavMeshAgent _agent;
    private Animator _animator;

    //Logic
    private Vector3 wander_point;
    private Vector3 last_point;
    private bool has_point = false;
    public bool attacked = false;



    //Rabbit should wander with every x seconds
    public float wander_cooldown = 2f;
    private float wander_delay;



    public RabbitWander(Rabbit rabbit)
    {
        _rabbit = rabbit;
        _agent = _rabbit.GetComponent<NavMeshAgent>();
        _animator = _rabbit.GetComponent<Animator>();
        wander_delay = wander_cooldown;
    }


    public void Tick()
    {
        //let rabbit wander with 2 sec delay
        if (wander_delay <= 0)
        {
            wander();
            wander_delay = wander_cooldown;

            //wabbit should be using the run animation here
            _animator.SetBool("isMoving", true);
        }
        else
        {
            //rabbit idles here
            wander_delay -= Time.deltaTime;
            _animator.SetBool("isMoving", false);
        }

        if (attacked)
        {
            //switch_state here
            Debug.Log("Should runaway here!");
            _rabbit.switch_state(_rabbit.rabbit_run);
        }


    }
    public void OnEnter()
    {
        attacked = false;
        has_point = false;
        _rabbit.attacker = null;
    }
    public void OnExit()
    {

    }

    public void wander()
    {
        //rabbits should wander around in search of one of three things:
        //1. Food (Carrots)
        //2. Mates (other rabbits)
        //3. Shelter (borrow)

        //Different behaviors should apply for finding each thing

        if (!has_point)
        {
            find_wander_point();
        }

        if (has_point)
        {
            if (wander_point != last_point)
            {
                _agent.SetDestination(wander_point);
                last_point = wander_point;
            }
        }

        if (Vector3.Distance(_rabbit.transform.position, wander_point) < 1f)
        {
            has_point = false;
        }

    }

    public void find_wander_point()
    {
        float randomX = Random.Range(-_rabbit.wander_range, _rabbit.wander_range);
        float randomZ = Random.Range(-_rabbit.wander_range, _rabbit.wander_range);

        Vector3 this_position = _rabbit.transform.position;
        Vector3 point = new Vector3(this_position.x + randomX, this_position.y, this_position.z + randomZ);

        if (Physics.Raycast(point, -_rabbit.transform.up, _rabbit.terrain_Layer))
        {
            wander_point = point;
            has_point = true;
        }
    }
}
