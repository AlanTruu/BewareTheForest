using UnityEngine;
using UnityEngine.AI;

public class WolfAttack : IState
{
    //References
    private Wolf _wolf;
    private NavMeshAgent _agent;
    private Animator _animator;
    private ILife target_life;
    private float attacking_distance = 3f;

    public WolfAttack(Wolf wolf, NavMeshAgent agent, Animator animator)
    {
        _wolf = wolf;
        _agent = agent;
        _animator = animator;

    }

    public void Tick()
    {
        // _animator.SetFloat("speed", _agent.velocity.magnitude);

        if (_wolf.target == null)
        {
            _wolf.switch_state(_wolf.wolf_patrol);
        }
        else
        {
            //get target life:
            target_life = _wolf.target.GetComponent<ILife>();

            float distance = Vector3.Distance(_wolf.transform.position, _wolf.target.transform.position);

            //wolf should rotate to face target
            Transform target = _wolf.target;
            Vector3 relativePos = target.position - _wolf.transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            _wolf.transform.rotation = rotation;

            //if target is within x distance, initiate an attack
            if (distance <= attacking_distance)
            {
                if (_wolf.can_attack)
                {
                    attack();
                }
            }
            //if player is not within 3 but is within 10 units, switch back to chasing
            //Must make use of is_alive functions, because the destruction of a life might be delayed
            else if (distance < 10f && target_life.is_alive())
            {
                Debug.Log("Reverting back to chase state");
                _wolf.switch_state(_wolf.wolf_chase);
            }
        }


    }

    public void OnEnter()
    {
        _animator.SetInteger("state", 2);
    }
    public void OnExit()
    {

    }

    public void attack()
    {
        _animator.SetTrigger("attack_trigger");
        target_life.take_damage(3f, _wolf.transform);
        _wolf.can_attack = false;
        _wolf.call_reset_attack(2f);
    }

}
