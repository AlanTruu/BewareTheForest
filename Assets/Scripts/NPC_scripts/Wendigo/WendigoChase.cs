using UnityEngine;
using UnityEngine.AI;

public class WendigoChase : IState
{
    private Wendigo _wendigo;
    private Animator _animator;
    private NavMeshAgent _agent;
    private GameObject _player;


    public WendigoChase(Wendigo wendigo)
    {
        _wendigo = wendigo;
        _animator = wendigo.animator;
        _agent = wendigo.agent;
        _player = SuperManager.player;
    }


    public void Tick()
    {
        float proximity = Vector3.Distance(_player.transform.position, _wendigo.transform.position);

        if (proximity <= 4f)
        {
            _agent.SetDestination(_wendigo.transform.position);
            _wendigo.switch_state(_wendigo.wendigo_attack);
        }
        else
        {
            _agent.SetDestination(_player.transform.position);
        }

        _animator.SetFloat("speed", _agent.velocity.magnitude);
    }

    public void OnEnter()
    {

    }

    public void OnExit()
    {

    }
}
