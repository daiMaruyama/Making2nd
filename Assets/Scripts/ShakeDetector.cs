using UnityEngine;

public class ShakeDetector : MonoBehaviour
{
    public float accelerationThreshold = 1.0f; // iPhone用
    void Update()
    {
#if UNITY_EDITOR
        // PCならクリックで仮動作
        if (Input.GetMouseButtonDown(0))
        {
            ShakeNearestStar();
        }
#else
        // iPhone加速度判定
        if (Input.acceleration.magnitude > accelerationThreshold)
        {
            ShakeNearestStar();
        }
#endif
    }

    /*void ShakeNearestStar()
    {
        // 仮: 最初の未振動星を振る
        StarController star = FindObjectOfType<StarController>();
        if (star != null && !star.IsShaken)
            star.Shake();
    }*/
    void ShakeNearestStar()
    {
        StarController star = Object.FindFirstObjectByType<StarController>();

        if (star != null && !star.IsShaken)
            star.Shake();
    }

}
