using UnityEngine;
using UnityEngine.AI;

public class WolfChase : IState
{
    Wolf _wolf;
    NavMeshAgent _agent;
    Animator _animator;
    
    public WolfChase(Wolf wolf, NavMeshAgent agent, Animator animator)
    {
        _wolf = wolf;
        _agent = agent;
        _animator = animator;

    }

    public void Tick() {}

    public void OnEnter()
    {
        _agent.SetDestination(_wolf.player_Character.position);
    }

    public void OnExit()
    {
        _agent.SetDestination(_wolf.transform.position);
    }
}
