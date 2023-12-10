using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlickeringLight : MonoBehaviour
{
    public Light2D lightSource; // Reference to the Light component
    public float maxIntensity = 1.0f; // Maximum intensity of the light
    public float minIntensity = 0.0f; // Minimum intensity of the light
    public float blinkSpeed = 1.0f; // Speed of blinking

    private float targetIntensity; // Target intensity to move towards

    void Start()
    {
        targetIntensity = maxIntensity;
    }

    void Update()
    {
        // Adjust light intensity
        lightSource.intensity = Mathf.MoveTowards(lightSource.intensity, targetIntensity, blinkSpeed * Time.deltaTime);

        // Change direction of intensity change if limits are reached
        if (lightSource.intensity >= maxIntensity)
        {
            targetIntensity = minIntensity;
        }
        else if (lightSource.intensity <= minIntensity)
        {
            targetIntensity = maxIntensity;
        }
    }
}
