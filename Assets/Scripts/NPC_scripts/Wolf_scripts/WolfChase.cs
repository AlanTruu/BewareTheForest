using UnityEngine;
using UnityEngine.AI;

public class WolfChase : IState
{
    private Wolf _wolf;
    private NavMeshAgent _agent;
    private Animator _animator;


    public WolfChase(Wolf wolf, NavMeshAgent agent, Animator animator)
    {
        _wolf = wolf;
        _agent = agent;
        _animator = animator;

    }

    public void Tick()
    {
        _animator.SetFloat("speed", _agent.velocity.magnitude);

        float distance = Vector3.Distance(_wolf.transform.position, _wolf.player_Character.transform.position);

        //Keep chasing if within 6-10 meters
        if (distance < 10f && distance > 6f)
        {
            _agent.SetDestination(_wolf.player_Character.transform.position);
        }

        //must switch to attack state if close enough to player
        if (distance <= 3f)
        {
            _wolf.switch_state(_wolf.wolf_attack);
        }

        //Give up chase and return to patrolling if player gets too far
        if (distance > 10f)
        {
            Debug.Log("Player too far, giving up chase");
            _wolf.switch_state(_wolf.wolf_patrol);
        }

    }

    public void OnEnter()
    {
        _agent.SetDestination(_wolf.player_Character.position);
        _animator.SetInteger("state", 1);
        Debug.Log("Entering chase");
    }

    public void OnExit()
    {

    }
}
