using System;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.AI;

public class RabbitRun : IState
{
    //References
    private Rabbit _rabbit;
    private NavMeshAgent _agent;
    private Animator _animator;


    //Logic
    private float threat_distance = 20f;
    private float run_distance = 15f;
    private Vector3 flee_destination;


    public RabbitRun(Rabbit rabbit)
    {
        _rabbit = rabbit;
        _agent = _rabbit.agent;
        _animator = _rabbit.animator;
    }

    public void Tick()
    {
        if (_rabbit.attacker)
        {

            //if attacker is far away, go back to wandering
            //else if made it to flee destination, go back to wandering
            bool escaped = Vector3.Distance(_rabbit.transform.position, _rabbit.attacker.transform.position) > threat_distance;
            bool made_it_to_destination = flee_destination != null && Vector3.Distance(_rabbit.transform.position, flee_destination) < 2f;

            if (escaped || made_it_to_destination)
            {
                _rabbit.switch_state(_rabbit.rabbit_wander);
            }
        }


    }
    public void OnEnter()
    {
        Debug.Log("Entering Rabbit flee OnEnter");
        _animator.SetBool("isMoving", true);
        run_away();
    }
    public void OnExit()
    {

    }

    public void run_away()
    {
        if (_rabbit.attacker)
        {
            Debug.Log("In runaway");
            Vector3 flee_direction = (_rabbit.transform.position - _rabbit.attacker.transform.position).normalized;
            flee_destination = _rabbit.transform.position + (flee_direction * run_distance);
            _agent.SetDestination(flee_destination);
        }
    }
}
