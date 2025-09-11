using UnityEngine;
using UnityEngine.InputSystem;

public class PressToScreen : MonoBehaviour
{
    [Header("共通のシーン遷移コントローラ")]
    [SerializeField] private SceneController transitionController;

    bool isPressed = false;

    void Update()
    {
        if (isPressed) return;

        // クリック or タップ検知
        if ((Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame) ||
            (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame))
        {
            isPressed = true;
            transitionController.StartSceneTransition();
        }
    }
}
