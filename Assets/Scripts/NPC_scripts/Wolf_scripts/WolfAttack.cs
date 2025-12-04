using UnityEngine;
using UnityEngine.AI;

public class WolfAttack : IState
{
    private Wolf _wolf;
    private NavMeshAgent _agent;
    private Animator _animator;

    public WolfAttack(Wolf wolf, NavMeshAgent agent, Animator animator)
    {
        _wolf = wolf;
        _agent = agent;
        _animator = animator;

    }

    public void Tick()
    {
        // _animator.SetFloat("speed", _agent.velocity.magnitude);
        
        float distance = Vector3.Distance(_wolf.transform.position, _wolf.player_Character.transform.position);
        
        if (distance <= 3f)
        {
            // attack here 
        }
        else if (distance < 10f)
        {
            //go back to chasing
            _wolf.switch_state(_wolf.wolf_chase);
        }
        else
        {
            //wolf quits and goes back to patrolling
            _wolf.switch_state(_wolf.wolf_patrol);
        }
    }

    public void OnEnter()
    {
        _animator.SetInteger("state", 2);
        Debug.Log("Entered attack, state should be 2");
        Debug.Log(_animator.GetInteger("state"));
    }
    public void OnExit()
    {
        
    }

}
