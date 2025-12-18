using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance;

    [Header("Settings")]
    public LayerMask placementMask;     // Terrain layer
    public float maxBuildDistance = 20f;

    [Header("Preview Materials")]
    public Material validMat;
    public Material invalidMat;

    private GameObject preview;
    private GameObject realPrefab;
    private Renderer[] previewRenderers;

    private Bounds previewBounds;
    private bool canPlace = false;
    private float currentRotation = 0f;

    private void Awake()
    {
        Instance = this;
    }

    public void StartPlacement(GameObject prefab)
    {
        realPrefab = prefab;

        // Create preview object
        preview = Instantiate(prefab);
        preview.name = prefab.name + "_Preview";

        // Disable any colliders on preview
        foreach (Collider col in preview.GetComponentsInChildren<Collider>())
            col.enabled = false;

        // Cache renderers for material swapping
        previewRenderers = preview.GetComponentsInChildren<Renderer>();

        // Build combined mesh bounds for collision checking
        previewBounds = new Bounds(preview.transform.position, Vector3.zero);
        foreach (Renderer r in previewRenderers)
            previewBounds.Encapsulate(r.bounds);

        currentRotation = 0f;
    }

    private void Update()
    {
        if (preview == null) return;

        UpdatePreviewPosition();
        HandleRotation();
        TryPlaceObject();
    }

    private void UpdatePreviewPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * maxBuildDistance, Color.red);

        // Raycast against EVERYTHING
        if (!Physics.Raycast(ray, out RaycastHit hit, maxBuildDistance))
        {
            preview.SetActive(false);
            canPlace = false;
            return;
        }

        // Always show preview at hit point
        preview.SetActive(true);

        preview.transform.position = hit.point + Vector3.up * 0.02f;
        preview.transform.rotation = Quaternion.Euler(0f, currentRotation, 0f);

        // Recalculate bounds AFTER moving
        previewBounds = new Bounds(preview.transform.position, Vector3.zero);
        foreach (Renderer r in previewRenderers)
            previewBounds.Encapsulate(r.bounds);

        // Collision check decides validity
        canPlace = !Physics.CheckBox(
            previewBounds.center,
            previewBounds.extents * 0.95f,
            preview.transform.rotation
        );

        UpdatePreviewMaterial(canPlace);
    }




    private void UpdatePreviewMaterial(bool valid)
    {
        foreach (Renderer r in previewRenderers)
            r.material = valid ? validMat : invalidMat;
    }

    private void HandleRotation()
    {
        if (Input.GetKey(KeyCode.Q))
            currentRotation -= 90 * Time.deltaTime;

        if (Input.GetKey(KeyCode.E))
            currentRotation += 90 * Time.deltaTime;

        currentRotation = Mathf.Round(currentRotation / 90f) * 90f;
    }

    private void TryPlaceObject()
    {
        // LEFT CLICK to place
        if (Input.GetMouseButtonDown(0) && canPlace)
        {
            Instantiate(realPrefab, preview.transform.position, preview.transform.rotation);
            Destroy(preview); // stop preview after placing
        }

        // RIGHT CLICK to cancel
        if (Input.GetMouseButtonDown(1))
        {
            Destroy(preview);
        }
    }
}
