using UnityEngine;
using System.Collections;

public class RifleScript : MonoBehaviour
{
    private Rigidbody rb;
    private bool isHeld = false;

    [Header("References")]
    private Camera playerCamera;
    public ParticleSystem muzzleFlash;
    public AudioSource gunAudio;
    public AudioClip gunShotClip;
    public GameObject tracerPrefab;
    public Transform muzzlePoint;
    public float tracerSpeed = 300f;
    
    [Header("Gun Settings")]
    public float damage = 5f;
    public float range = 100f;
    public float fireRate = 10f;      // Bullets per second
    private float nextTimeToFire = 0f;

    [Header("Impact")]
    public GameObject impactEffect;
    
    void Start()
    {
        playerCamera = Camera.main;
    }
    
    void Update()
    {
        // Check if this object is a child of an object named "Hand"
        if (IsChildOfName(transform, "Hand"))
        {
            if (!isHeld)
            {
                rb = GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = false;   // disable physics
                    rb.useGravity = false;     // disable gravity
                }
                isHeld = true;
            }

            // Left mouse click triggers action
            if (Input.GetMouseButtonDown(0))
            {
                DoAction();
            }
        }
        else
        {
            isHeld = false;
        }
    }

    void DoAction()
    {
        Debug.Log("Shots fired!");
        // Replace this with action
        Shoot();
    }

    void Shoot()
    {
        // Play effects
        if (muzzleFlash != null) muzzleFlash.Play();
        if (gunAudio != null) gunAudio.PlayOneShot(gunShotClip);

        // Raycast forward
        RaycastHit hit;
        Vector3 start = playerCamera.transform.position + playerCamera.transform.forward * .3f;
        if (Physics.Raycast(start, playerCamera.transform.forward, out hit, range))
        {
            StartCoroutine(SpawnTracer(hit.point));
            // Hit an object with ILife/Health
            ILife life = hit.transform.GetComponent<ILife>();
            if (life != null)
            {
                life.take_damage(damage);
            }

            // Optional hit particles
            if (impactEffect != null)
            {
                GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impact, 1f);
            }
        }
        else
        {
            StartCoroutine(SpawnTracer(playerCamera.transform.position + playerCamera.transform.forward * 100f));
        }
    }

    IEnumerator SpawnTracer(Vector3 target)
    {
        GameObject tracerObj = Instantiate(tracerPrefab, muzzlePoint.position, Quaternion.identity);
        TrailRenderer trail = tracerObj.GetComponent<TrailRenderer>();

        //Vector3 start = muzzlePoint.position;
        yield return null;

        //float distance = Vector3.Distance(start, target);
        //float remaining = distance;
        float remaining = Vector3.Distance(tracerObj.transform.position, target);

        while (remaining > 0f)
        {
            float step = tracerSpeed * Time.deltaTime;
            tracerObj.transform.position = Vector3.MoveTowards(tracerObj.transform.position, target, step);
            remaining -= step;
            yield return null;
        }

        tracerObj.transform.position = target;

        // destroy after trail finishes
        Destroy(tracerObj, trail.time);
    }

    // Check recursively if this object is a child of a parent with the specified name
    private bool IsChildOfName(Transform child, string parentName)
    {
        while (child != null)
        {
            if (child.name == parentName)
                return true;
            child = child.parent;
        }
        return false;
    }
}
