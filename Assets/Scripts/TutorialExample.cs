using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialExample : MonoBehaviour
{
    [SerializeField] private TutorialTextController tutorial;

    private void Start()
    {
        // タイトル
        tutorial.ShowMessage(" Press Any Button ");
    }

    private void Update()
    {
        // 新InputSystem
        if (Mouse.current.leftButton.wasPressedThisFrame ||
            Touchscreen.current?.primaryTouch.press.wasPressedThisFrame == true)
        {
            tutorial.HideMessage();
            // tutorial.ShowMessage("振れ！");
        }
    }
}
