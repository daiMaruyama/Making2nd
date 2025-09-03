using UnityEngine;

/// <summary>
/// Attach to the Main Camera. Handles camera shake effect.
/// </summary>
public class CameraController : MonoBehaviour
{
    public float m_shakeIntensity = .3f;
    public float m_shakeDecay = .3f;

    private Vector3 m_originPosition;
    private float decay;
    private float intensity;

    void Start()
    {
        m_originPosition = transform.position; // remember starting pos
    }

    void Update()
    {
        if (intensity > 0)
        {
            transform.position = m_originPosition + Random.insideUnitSphere * intensity;
            intensity -= decay * Time.deltaTime;

            if (intensity <= 0)
                transform.position = m_originPosition;
        }
    }

    /// <summary>
    /// Trigger a camera shake. 
    /// You can set intensity based on acceleration, input, etc.
    /// </summary>
    public void Shake(float customIntensity = -1f)
    {
        if (intensity <= 0)
            m_originPosition = transform.position;

        intensity = (customIntensity > 0) ? customIntensity : m_shakeIntensity;
        decay = m_shakeDecay;
    }
}
