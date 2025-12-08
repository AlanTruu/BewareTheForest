using UnityEngine;
using UnityEngine.AI;

public class Follower_script : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private NavMeshAgent agent;
    [SerializeField] Transform player;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (player)
        {
            agent.SetDestination(player.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
