using UnityEngine;
using UnityEngine.AI;

[DisallowMultipleComponent]
public class Enemy_references : MonoBehaviour
{
    [HideInInspector] public NavMeshAgent agent;  
    [HideInInspector] public Animator animator;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
