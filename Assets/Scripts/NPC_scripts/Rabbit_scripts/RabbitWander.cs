using UnityEngine;
using UnityEngine.AI;

public class RabbitWander : IState
{
    Rabbit _rabbit;
    private Vector3 wander_point;
    private Vector3 last_point;
    private bool has_point = false;


    private NavMeshAgent _agent;

    public RabbitWander(Rabbit rabbit)
    {
        _rabbit = rabbit;
        _agent = _rabbit.GetComponent<NavMeshAgent>();
    }


    public void Tick()
    {
        wander();
    }
    public void OnEnter()
    {

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
