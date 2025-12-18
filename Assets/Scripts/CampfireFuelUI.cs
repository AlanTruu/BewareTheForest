using UnityEngine;
using UnityEngine.UI;

public class CampfireFuelUI : MonoBehaviour
{
    [Header("References")]
    public Campfire campfire;
    public Image fuelFillImage;
    public Transform player;

    [Header("Visibility")]
    public float showDistance = 6f;

    private CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0;
        }
    }

    private void Update()
    {
        if (campfire == null || player == null || fuelFillImage == null)
            return;

        // Distance check
        float distance = Vector3.Distance(player.position, campfire.transform.position);
        bool show = distance <= showDistance;

        if (canvasGroup != null)
        {
            canvasGroup.alpha = show ? 1f : 0f;
        }

        // Fuel update
        float fillAmount = campfire.fuel / campfire.maxFuel;
        fuelFillImage.fillAmount = fillAmount;
    }
}
