using UnityEngine;
using UnityEngine.Serialization;

public class LightScript : MonoBehaviour, IInteractable
{
    [SerializeField] private float intensityOn = 1f;
    [SerializeField] private bool smoothTransition = true;
    [SerializeField, Range(0.01f, 5f)] private float transitionDuration = 1f;
    [SerializeField] private bool isBroken = false;
    [SerializeField, Range(1f, 20f)] private float brokenFlickerSpeed = 1f;
    [SerializeField, Range(1f, 10f)] private float brokenIntensityMultiplier = 1f;
    private float currentIntensity = 0f;
    private float targetIntensity = 0f;
    private Light lightObject;
    private float intensityChangedVelocity = 0f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        lightObject = GetComponent<Light>();
        intensityOn = lightObject.intensity;
        lightObject.intensity = 0f;
    }

    // Update is called once per frame
    private void Update()
    {
        currentIntensity = smoothTransition ? Mathf.SmoothDamp(currentIntensity, targetIntensity, ref intensityChangedVelocity, Mathf.Max(0.0001f, transitionDuration)) : currentIntensity;
        if (isBroken)
            currentIntensity = BrokenLight();
        lightObject.intensity = currentIntensity;
    }

    public void Interact()
    {
        targetIntensity = intensityOn;
        Debug.Log("Light Intensity set to: " + targetIntensity);
        
    }

    private float BrokenLight()
        {
            float perlin = Mathf.PerlinNoise1D(Time.time * brokenFlickerSpeed);
            return Mathf.Clamp(perlin, 0f, 1f) * targetIntensity * brokenIntensityMultiplier;
        }
    // Not used for Light
    public void UnInteract() { }

    // Not used for Light
    public void ExitInteractArea() { }
}
