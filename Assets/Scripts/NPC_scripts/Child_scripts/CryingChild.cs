using System;
using System.Collections;
using UnityEditor.Analytics;
using UnityEngine;
using UnityEngine.AI;

public class CryingChild : MonoBehaviour, ILife
{
    public float health = 5f;


    //References
    public NavMeshAgent agent;

    //States
    private IState current_state;
    public ChildIdle child_idle;
    public ChildFollow child_follow;

    //Logic
    public bool is_dead = false;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }


    void Start()
    {
        child_idle = new ChildIdle(this);
        child_follow = new ChildFollow(this);
        current_state = child_idle;
    }

    // Update is called once per frame
    void Update()
    {
        current_state.Tick();

    }

    public void switch_state(IState state)
    {
        current_state = state;
    }

    public void take_damage(float damage, Transform source = null)
    {
        health -= damage;

        if (health <= 0 && !is_dead)
        {
            is_dead = true;
            die();
        }
    }

    public void die()
    {
        agent.SetDestination(this.transform.position);
        StartCoroutine(Death());
    }

    IEnumerator Death()
    {
        Quaternion start = transform.rotation;
        Quaternion end = Quaternion.Euler(transform.eulerAngles + new Vector3(-90, 0, 0));

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * 2f;
            transform.rotation = Quaternion.Slerp(start, end, t);
            yield return null;
        }
        Destroy(this.gameObject);
    }

}
