using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public Light lightSource;
    public float minFlickerDelay = 0.1f; // Minimum time between flickers
    public float maxFlickerDelay = 0.5f; // Maximum time between flickers

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
            lightSource.enabled = !lightSource.enabled;
            yield return new WaitForSeconds(delay);
        }
    }
}
