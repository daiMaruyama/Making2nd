using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialExample : MonoBehaviour
{
    [SerializeField] private TutorialTextController tutorial;

    private void Start()
    {
        tutorial.ShowMessage(" 画面をタップしてください ");
    }

    private void Update()
    {
        // 新InputSystem
        if (Mouse.current.leftButton.wasPressedThisFrame ||
            Touchscreen.current?.primaryTouch.press.wasPressedThisFrame == true)
        {
            tutorial.HideMessage();
        }
    }
}
