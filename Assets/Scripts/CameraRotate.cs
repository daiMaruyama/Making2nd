using UnityEngine;
using UnityEngine.InputSystem; // êVInput System

public class CameraRotate : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f;

    private void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.isPressed)
        {
            Vector2 delta = Mouse.current.delta.ReadValue();

            float x = delta.x * rotationSpeed * Time.deltaTime;
            float y = -delta.y * rotationSpeed * Time.deltaTime;

            transform.Rotate(Vector3.up, x, Space.World);
            transform.Rotate(Vector3.right, y, Space.Self);
        }
    }
}
