using UnityEngine;

/// <summary>
/// Attach to the Main Camera. Handles camera shake effect + skybox motion.
/// </summary>
public class CameraController : MonoBehaviour
{
    [Header("Camera Shake")]
    public float m_shakeIntensity = .3f;
    public float m_shakeDecay = .3f;

    [Header("Skybox Motion")]
    public float skyboxShakeIntensity = .2f;   // ‰ñ“]—h‚ê‚Ì‹­‚³
    public float skyboxShakeDecay = .2f;       // Œ¸Š‚Ì‘¬‚³

    private Vector3 m_originPosition;
    private float decay;
    private float intensity;

    private float skyboxRotationOrigin;
    private float skyboxIntensity;
    private float skyboxDecay;

    void Start()
    {
        m_originPosition = transform.position; // remember starting pos
        if (RenderSettings.skybox != null && RenderSettings.skybox.HasProperty("_Rotation"))
        {
            skyboxRotationOrigin = RenderSettings.skybox.GetFloat("_Rotation");
        }
    }

    void Update()
    {
        // ƒJƒƒ‰—h‚ê
        if (intensity > 0)
        {
            transform.position = m_originPosition + Random.insideUnitSphere * intensity;
            intensity -= decay * Time.deltaTime;

            if (intensity <= 0)
                transform.position = m_originPosition;
        }

        // Skybox—h‚ê
        if (skyboxIntensity > 0 && RenderSettings.skybox != null && RenderSettings.skybox.HasProperty("_Rotation"))
        {
            float randomOffset = Random.Range(-skyboxIntensity, skyboxIntensity);
            RenderSettings.skybox.SetFloat("_Rotation", skyboxRotationOrigin + randomOffset);

            skyboxIntensity -= skyboxDecay * Time.deltaTime;

            if (skyboxIntensity <= 0)
                RenderSettings.skybox.SetFloat("_Rotation", skyboxRotationOrigin);
        }
    }

    /// <summary>
    /// Trigger a camera shake. 
    /// </summary>
    public void Shake(float customIntensity = -1f)
    {
        if (intensity <= 0)
            m_originPosition = transform.position;

        intensity = (customIntensity > 0) ? customIntensity : m_shakeIntensity;
        decay = m_shakeDecay;

        // Skybox ‚à—h‚ç‚·
        skyboxIntensity = skyboxShakeIntensity;
        skyboxDecay = skyboxShakeDecay;
    }
}
