using UnityEngine;
using UnityEngine.AI;

public class WolfAttack : IState
{
    private Wolf _wolf;
    private NavMeshAgent _agent;
    private Animator _animator;
    private PlayerData player_data;

    public WolfAttack(Wolf wolf, NavMeshAgent agent, Animator animator)
    {
        _wolf = wolf;
        _agent = agent;
        _animator = animator;
        player_data = _wolf.player_Character.GetComponent<PlayerData>();

    }

    public void Tick()
    {
        // _animator.SetFloat("speed", _agent.velocity.magnitude);

        float distance = Vector3.Distance(_wolf.transform.position, _wolf.player_Character.transform.position);

        //wolf should rotate to face player
        Transform player = _wolf.player_Character;
        Vector3 relativePos = player.position - _wolf.transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        _wolf.transform.rotation = rotation;

        //if player is within x distance, initiate an attack
        if (distance <= 2.5f)
        {
            if (_wolf.can_attack)
            {
                _animator.SetTrigger("attack_trigger");
                _wolf.can_attack = false;
                _wolf.call_reset_attack(2f);

                //Check if wolf landed attack, or do damage as long as player was in range?
                //For now, player will take damage as long as this if branch is taken
                player_data.take_damage(3f, "Wolf");
            }
        }
        //if player is not within 3 but is within 10 units, switch back to chasing
        else if (distance < 10f)
        {

            _wolf.switch_state(_wolf.wolf_chase);
        }
        //wolf quits and goes back to patrolling
        else
        {

            _wolf.switch_state(_wolf.wolf_patrol);
        }
    }

    public void OnEnter()
    {
        _animator.SetInteger("state", 2);
    }
    public void OnExit()
    {

    }

}
