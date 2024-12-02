using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public Light lightSource;
    public float minFlickerDelay = 0.1f; // Minimum time between flickers
    public float maxFlickerDelay = 0.5f; // Maximum time between flickers
    public float minIntensity = 0.2f; // Minimum intensity for the dim effect
    public float maxIntensity = 1f; // Maximum intensity for the light

    private void Start()
    {
        if (lightSource == null)
        {
            lightSource = GetComponent<Light>();
        }

        if (lightSource != null)
        {
            StartCoroutine(FlickerLight());
        }
        else
        {
            Debug.LogWarning("No Light component assigned or found!");
        }
    }

    private System.Collections.IEnumerator FlickerLight()
    {
        while (true)
        {
            float delay = Random.Range(minFlickerDelay, maxFlickerDelay);
            lightSource.intensity = Random.Range(minIntensity, maxIntensity);
            yield return new WaitForSeconds(delay);
        }
    }
}
