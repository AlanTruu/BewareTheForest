//using System.Numerics;
//using System;
//using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;


public class Spawner : MonoBehaviour
{
    [SerializeField] Transform entity_prefab;
    [SerializeField] Transform alt_prefab;
    [SerializeField] LayerMask detectable_layer;
    [SerializeField] string entity_tag;
    public float y_ground; //used to detect where the ground is
    public float alt_chance = 0.2f; //probability of spawning an alt instead of the default
    private Transform entity;

    //Controls how many entities are allowed to be near the spawner
    public int entity_limit = 5;
    private bool below_limit = true;
    private int number_to_spawn = 0;

    //Range at which an entity spawns relative to spawner position
    public float spawn_range = 10f;

    //Max Range at which entities count as being close to the spawner
    public float capacity_bound = 20f;

    //Controls how often the spawner should do a head count
    public float count_cd = 20f;
    private float count_timer = 0;

    void Start()
    {
        Vector3 ground_pos = GetGroundedPosition(transform.position);
        y_ground = ground_pos.y;
    }

    // Update is called once per frame
    void Update()
    {
        //Do an entity count
        if (count_timer <= 0)
        {
            //Call entity count here
            entity_count();
            count_timer = count_cd;
        }
        else
        {
            //Decrement timer
            count_timer -= Time.deltaTime;
        }

        if (number_to_spawn > 0)
        {
            spawn_multiple(number_to_spawn);
            number_to_spawn = 0;
        }

    }

    void spawn()
    {
        float randomX = Random.Range(-spawn_range, spawn_range);
        float randomZ = Random.Range(-spawn_range, spawn_range);

        Vector3 pos = transform.position;

        Vector3 spawn_pos = new Vector3(pos.x + randomX, y_ground, pos.z + randomZ);

        Transform prefab_to_spawn = entity_prefab;

        if (alt_prefab != null && Random.Range(0f, 1f) <= alt_chance)
        {
            prefab_to_spawn = alt_prefab;
        }

        entity = Instantiate(prefab_to_spawn, spawn_pos, Quaternion.identity);

        NavMeshAgent agent = entity.GetComponent<NavMeshAgent>();

        if (agent != null)
        {
            agent.Warp(spawn_pos);
        }
    }

    void spawn_multiple(int count)
    {
        for (int i = 0; i < count; i++)
        {
            spawn();
        }
    }

    void entity_count()
    {
        //Find surrounding entities by entity tag
        //Count the number that is within 

        Collider[] colliders = Physics.OverlapSphere(transform.position, capacity_bound, detectable_layer);
        int head_count = 0;

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag(entity_tag))
            {
                head_count += 1;
            }
        }

        if (head_count < entity_limit)
        {
            below_limit = true;
            number_to_spawn = entity_limit - head_count;
        }
        else
        {
            below_limit = false;
        }
    }

    public Vector3 GetGroundedPosition(Vector3 spawnXZ, float rayHeight = 50f)
    {
        Vector3 rayOrigin = spawnXZ + Vector3.up * rayHeight;

        if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, rayHeight * 2f))
        {
            return hit.point;
        }

        return spawnXZ; // fallback
    }

}
