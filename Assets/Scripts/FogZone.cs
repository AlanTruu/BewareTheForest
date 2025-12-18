using UnityEngine;

public class FogZone : MonoBehaviour
{
    public float clearFogDensity = 0.002f;
    public float darkFogDensity = 0.02f;
    public float transitionSpeed = 1f;

    private bool active = false;

    private void Update()
    {
        float target = active ? clearFogDensity : darkFogDensity;
        RenderSettings.fogDensity = Mathf.Lerp(
            RenderSettings.fogDensity,
            target,
            Time.deltaTime * transitionSpeed
        );
    }

    public void SetActive(bool value)
    {
        active = value;
    }
}
