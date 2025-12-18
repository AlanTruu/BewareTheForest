using UnityEngine;

public class BuildTest : MonoBehaviour
{
    public GameObject prefab;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            BuildingManager.Instance.StartPlacement(prefab);
        }
    }
}
