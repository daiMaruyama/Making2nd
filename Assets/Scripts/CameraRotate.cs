using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    [SerializeField] private float autoRotationSpeed = 5f;  // 自動回転の速さ
    // [SerializeField] private float manualRotationSpeed = 100f; // 手動操作の速さ

    private void Update()
    {
        // === 自動回転 ===
        transform.Rotate(Vector3.up, autoRotationSpeed * Time.deltaTime, Space.World);

        /* // === 手動回転 (マウス or タッチ) ===
        if (UnityEngine.InputSystem.Mouse.current != null &&
            UnityEngine.InputSystem.Mouse.current.leftButton.isPressed)
        {
            float x = UnityEngine.InputSystem.Mouse.current.delta.x.ReadValue() * manualRotationSpeed * Time.deltaTime;
            float y = -UnityEngine.InputSystem.Mouse.current.delta.y.ReadValue() * manualRotationSpeed * Time.deltaTime;

            transform.Rotate(Vector3.up, x, Space.World);
            transform.Rotate(Vector3.right, y, Space.Self);
        }

        if (UnityEngine.InputSystem.Touchscreen.current != null &&
            UnityEngine.InputSystem.Touchscreen.current.primaryTouch.press.isPressed)
        {
            Vector2 delta = UnityEngine.InputSystem.Touchscreen.current.primaryTouch.delta.ReadValue();
            float x = delta.x * manualRotationSpeed * Time.deltaTime;
            float y = -delta.y * manualRotationSpeed * Time.deltaTime;

            transform.Rotate(Vector3.up, x, Space.World);
            transform.Rotate(Vector3.right, y, Space.Self);
        }*/
    }
}
