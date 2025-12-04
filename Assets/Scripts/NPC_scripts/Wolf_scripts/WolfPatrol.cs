//using System.Numerics;
using Mono.Cecil;
using UnityEngine;
using UnityEngine.AI;

public class WolfPatrol : IState
{
    private Wolf _wolf;
    private NavMeshAgent _agent;
    private Animator _animator;

    private Vector3 patrol_point;
    private Vector3 last_point;
    private bool has_point = false;


    public WolfPatrol(Wolf wolf, NavMeshAgent agent, Animator animator)
    {
        _wolf = wolf;
        _agent = agent;
        _animator = animator;
    }

    public void Tick()
    {
        patrol();

        //initiate chase when player is within 10 meters
        if (Vector3.Distance(_wolf.transform.position, _wolf.player_Character.transform.position) < 10f)
        {
            _wolf.switch_state(_wolf.wolf_chase);
        }
    }

    public void OnEnter()
    {
        // _animator.SetBool("inAttackingRange", false);
        // _animator.SetBool("isPatrolling", true);

        _animator.SetInteger("state", 1);
    }

    public void OnExit()
    {
    }

    public void find_patrol_point()
    {
        //get random floats in patrol range
        float randomX = Random.Range(-_wolf.patrol_range, _wolf.patrol_range);
        float randomZ = Random.Range(-_wolf.patrol_range, _wolf.patrol_range);

        Vector3 this_position = _wolf.transform.position;
        Vector3 point = new Vector3(this_position.x + randomX, this_position.y, this_position.z + randomZ);

        //Check if patrol point is valid
        if (Physics.Raycast(point, -_wolf.transform.up, _wolf.terrain_Layer))
        {
            patrol_point = point;
            has_point = true;
        }
    }

    public void patrol()
    {
        if (!has_point)
        {
            find_patrol_point();
        }

        if (has_point)
        {
            if (patrol_point != last_point)
            {
                _agent.SetDestination(patrol_point);
                last_point = patrol_point;
                _animator.SetFloat("speed", _agent.velocity.magnitude);
            }
        }

        if (Vector3.Distance(_wolf.transform.position, patrol_point) < 1f)
        {
            has_point = false;
        }
    }
}
