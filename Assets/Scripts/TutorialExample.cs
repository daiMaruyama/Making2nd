using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialExample : MonoBehaviour
{
    [SerializeField] private TutorialTextController tutorial;

    private void Start()
    {
        tutorial.ShowMessage(" ��ʂ��^�b�v���Ă������� ");
    }

    private void Update()
    {
        // �VInputSystem
        if (Mouse.current.leftButton.wasPressedThisFrame ||
            Touchscreen.current?.primaryTouch.press.wasPressedThisFrame == true)
        {
            tutorial.HideMessage();
        }
    }
}
