using UnityEngine;

public class WoodenWallBuild : MonoBehaviour
{
    public GameObject prefabToBuild;
    private bool buildMode = false;

    private void Update()
    {
        // Return if the prefab is not on player's hand
        if (!IsChildOfName(transform, "Hand"))
        {
            return;
        }

        // Left click to start the building and call the build function
        if (Input.GetMouseButtonDown(0) && !buildMode)
        {
            // Return if no prefab exists
            if (prefabToBuild == null)
            {
                return;
            }

            BuildingManager.Instance.StartPlacement(prefabToBuild);
            buildMode = true;
        }

        // If Right click is pressed, cancel the build mode
        if (Input.GetMouseButtonDown(1))
        {
            buildMode = false;
        }
    }

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
