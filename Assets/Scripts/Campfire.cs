using UnityEngine;
using System.Collections;

public class Campfire : MonoBehaviour
{
    [Header("Fuel Settings")]
    public float fuel = 50f;
    public float maxFuel = 100f;
    public float burnRate = 1f; // fuel per second

    [Header("Scaling Difficulty")]
    public float maxFuelIncrease = 50f;
    public float burnRateMultiplier = 1.15f;

    [Header("Log Settings")]
    public ItemSO logItem;
    public float fuelPerLog = 20f;

    [Header("Effects")]
    public GameObject fireVisual;
    public Light fireLight;

    [Header("Fog Control")]
    public FogZone fogZone;

    [Header("Enemy")]
    public GameObject wendigoPrefab;
    public float spawnRadius = 20f;

    private bool isLit = true;
    private bool wendigoSpawned = false;

    private void Start()
    {
        UpdateFireState();
        StartCoroutine(BurnFuel());
    }

    private IEnumerator BurnFuel()
    {
        while (true)
        {
            if (isLit)
            {
                fuel -= burnRate * Time.deltaTime;

                if (fuel <= 0)
                {
                    fuel = 0;
                    Extinguish();
                }
            }

            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Item item = other.GetComponent<Item>();

        if (item == null) return;
        if (item.item != logItem) return;

        AddFuelFromLogs(item.amount);
        Destroy(other.gameObject);
    }

    private void AddFuelFromLogs(int logAmount)
    {
        float addedFuel = logAmount * fuelPerLog;
        fuel += addedFuel;

        // If fuel exceeds current max fuel
        if (fuel >= maxFuel)
        {
            maxFuel += maxFuelIncrease;
            burnRate *= burnRateMultiplier;
        }

        isLit = true;
        wendigoSpawned = false;
        UpdateFireState();
    }

    private void Extinguish()
    {
        if (!isLit) return;

        isLit = false;
        UpdateFireState();
        SpawnWendigo();
    }

    private void UpdateFireState()
    {
        fireVisual.SetActive(isLit);
        fireLight.enabled = isLit;

        if (fogZone != null)
        {
            fogZone.SetActive(isLit);
        }
    }

    private void SpawnWendigo()
    {
        if (wendigoSpawned) return;

        Vector3 randomPos = transform.position +
            Random.insideUnitSphere * spawnRadius;

        randomPos.y = transform.position.y;

        Instantiate(wendigoPrefab, randomPos, Quaternion.identity);
        wendigoSpawned = true;
    }
}
