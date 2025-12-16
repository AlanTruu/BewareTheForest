using System;
using NUnit.Framework;
using UnityEditor;
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
    private float explosion_damage = 10f;
    private bool is_monster;
    private bool has_exploded = false;



    public RabbitRun(Rabbit rabbit)
    {
        _rabbit = rabbit;
        _agent = _rabbit.agent;
        _animator = _rabbit.animator;
        is_monster = _rabbit.is_monster;
    }

    public void Tick()
    {
        if (_rabbit.attacker && !is_monster)
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
        else if (_rabbit.attacker)
        {
            // if monster

            if (Vector3.Distance(_rabbit.transform.position, _rabbit.attacker.transform.position) <= 2f)
            {
                explode();
            }
        }

    }
    public void OnEnter()
    {
        _animator.SetBool("isMoving", true);

        if (!is_monster)
        {
            run_away();
        }
        else
        {
            run_toward_attacker();
        }


    }
    public void OnExit()
    {

    }

    public void run_away()
    {
        if (_rabbit.attacker)
        {
            Vector3 flee_direction = (_rabbit.transform.position - _rabbit.attacker.transform.position).normalized;
            flee_destination = _rabbit.transform.position + (flee_direction * run_distance);
            _agent.SetDestination(flee_destination);
        }
    }

    public void run_toward_attacker()
    {
        if (_rabbit.attacker)
        {
            _agent.SetDestination(_rabbit.attacker.position);
        }
    }

    public void explode()
    {
        if (has_exploded) return;
        has_exploded = true;

        _rabbit.spawn_explosion();
    }
}
