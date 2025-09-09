using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialExample : MonoBehaviour
{
    [SerializeField] private TutorialTextController tutorial;

    private void Start()
    {
        // �^�C�g��
        tutorial.ShowMessage("Press Any Button");
    }

    private void Update()
    {
        // �VInputSystem
        if (Mouse.current.leftButton.wasPressedThisFrame ||
            Touchscreen.current?.primaryTouch.press.wasPressedThisFrame == true)
        {
            tutorial.HideMessage();
            // tutorial.ShowMessage("�U��I");
        }
    }
}
