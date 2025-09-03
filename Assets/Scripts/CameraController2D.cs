using UnityEngine;

public class CameraController2D : MonoBehaviour
{
    public float shakeIntensity = 0.3f;
    public float shakeDecay = 0.3f;

    private Vector3 originPosition;
    private float intensity;
    private float decay;

    void Start()
    {
        originPosition = transform.position;
    }

    void Update()
    {
        if (intensity > 0)
        {
            Vector2 offset = Random.insideUnitCircle * intensity;
            transform.position = new Vector3(originPosition.x + offset.x, originPosition.y + offset.y, originPosition.z);

            intensity -= decay * Time.deltaTime;
            if (intensity <= 0)
                transform.position = originPosition;
        }
    }

    public void Shake(float customIntensity = -1f)
    {
        if (intensity <= 0)
            originPosition = transform.position;

        intensity = (customIntensity > 0) ? customIntensity : shakeIntensity;
        decay = shakeDecay;
    }
}
