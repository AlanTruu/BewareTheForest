using UnityEngine;
using UnityEngine.AI;

public class WendigoChase : IState
{
    private Wendigo _wendigo;
    private Animator _animator;
    private NavMeshAgent _agent;
    private AudioSource audio;
    private GameObject _player;


    //Logic
    public float screech_cd = 8f;
    public float screech_count = 0f;


    public WendigoChase(Wendigo wendigo)
    {
        _wendigo = wendigo;
        _animator = wendigo.animator;
        _agent = wendigo.agent;
        _player = SuperManager.player;
        audio = wendigo.audio_source;
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

        screech_count -= Time.deltaTime;

        if (screech_count <= 0)
        {
            audio.PlayOneShot(_wendigo.screech);
            screech_count = screech_cd;
        }
    }

    public void OnEnter()
    {

    }

    public void OnExit()
    {

    }
}
