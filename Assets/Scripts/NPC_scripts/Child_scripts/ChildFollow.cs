using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ChildFollow : IState
{
    //References
    private CryingChild _child;
    private NavMeshAgent _agent;
    private GameObject player;

    private bool is_close_to_player = false;

    public ChildFollow(CryingChild child)
    {
        _child = child;
        _agent = child.agent;
        player = SuperManager.player;
    }

    public void Tick()
    {
        Debug.Log("TICK TOCK");

        float player_proximity = Vector3.Distance(_child.transform.position, player.transform.position);

        //check player player_proximity
        if (player_proximity < 2f)
        {
            return;
        }
        else
        {
            _agent.SetDestination(player.transform.position);
        }

    }
    public void OnEnter()
    {

    }
    public void OnExit()
    {

    }


}
