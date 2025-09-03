using UnityEngine;

public class ShakeDetector : MonoBehaviour
{
    public float accelerationThreshold = 1.0f; // iPhone�p
    void Update()
    {
#if UNITY_EDITOR
        // PC�Ȃ�N���b�N�ŉ�����
        if (Input.GetMouseButtonDown(0))
        {
            ShakeNearestStar();
        }
#else
        // iPhone�����x����
        if (Input.acceleration.magnitude > accelerationThreshold)
        {
            ShakeNearestStar();
        }
#endif
    }

    /*void ShakeNearestStar()
    {
        // ��: �ŏ��̖��U������U��
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
