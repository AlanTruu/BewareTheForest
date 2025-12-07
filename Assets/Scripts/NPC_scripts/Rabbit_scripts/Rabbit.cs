using UnityEngine;
using UnityEngine.AI;

public class Rabbit : MonoBehaviour, ILife
{
    public NavMeshAgent agent;
    public Animator animator;
    public IState current_state;
    public float wander_range = 2f;
    [SerializeField] public LayerMask terrain_Layer;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        current_state.Tick();
    }
}
