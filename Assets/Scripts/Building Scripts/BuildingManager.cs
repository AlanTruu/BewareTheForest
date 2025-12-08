using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance;

    [Header("Settings")]
    public LayerMask placementMask;  // ground and surfaces u can build on
    public float maxBuildDistance = 6f;

    [Header("Preview Materials")]
    public Material validMat;
    public Material invalidMat;

    private GameObject preview;
    private GameObject realPrefab;
    private Renderer[] previewRenderers;

    private bool canPlace = false;
    private float currentRotation = 0f;

    private void Awake()
    {
        Instance = this;
    }

    public void StartPlacement(GameObject prefab)
    {
        realPrefab = prefab;

        // makes a preview copy
        preview = Instantiate(prefab);
        preview.name = prefab.name + "_Preview";

        // removes scripts & colliders
        foreach (Collider col in preview.GetComponentsInChildren<Collider>())
            col.enabled = false;

        // get all renderers to swap materials
        previewRenderers = preview.GetComponentsInChildren<Renderer>();

        foreach (Renderer r in previewRenderers)
            r.material = validMat;

        currentRotation = 0f;
    }

    private void Update()
    {
        if (preview == null) return;

        HandlePreviewFollow();
        HandleRotation();
        HandlePlacement();
    }

    void HandlePreviewFollow()
    {
        // casts from center of screen
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f));
        if (Physics.Raycast(ray, out RaycastHit hit, maxBuildDistance, placementMask))
        {
            Vector3 pos = hit.point;

            // optional snap to grid:
            pos.x = Mathf.Round(pos.x / 0.5f) * 0.5f;
            pos.z = Mathf.Round(pos.z / 0.5f) * 0.5f;

            preview.transform.position = pos;
            preview.transform.rotation = Quaternion.Euler(0f, currentRotation, 0f);

            // checks for collisions:
            canPlace = !Physics.CheckBox(
                preview.transform.position,
                preview.GetComponentInChildren<Renderer>().bounds.extents * 0.95f,
                preview.transform.rotation
            );

            SwapPreviewMaterial(canPlace);
        }
    }

    void HandleRotation()
    {
        if (Input.GetKey(KeyCode.Q))
            currentRotation -= 90 * Time.deltaTime;

        if (Input.GetKey(KeyCode.E))
            currentRotation += 90 * Time.deltaTime;

        // clean rotation
        currentRotation = Mathf.Round(currentRotation / 90f) * 90f;
    }

    void HandlePlacement()
    {
        if (Input.GetMouseButtonDown(0) && canPlace)
        {
            Instantiate(realPrefab, preview.transform.position, preview.transform.rotation);
        }

        if (Input.GetMouseButtonDown(1)) // right click cancels
        {
            Destroy(preview);
        }
    }

    void SwapPreviewMaterial(bool valid)
    {
        foreach (Renderer r in previewRenderers)
            r.material = valid ? validMat : invalidMat;
    }
}
