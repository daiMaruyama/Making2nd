using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialExample : MonoBehaviour
{
    [SerializeField] private TutorialTextController tutorial;
    [SerializeField] private float waitTime = 1f;

    private void Start()
    {
        StartCoroutine(ShowDelay());
    }

    private IEnumerator ShowDelay()
    {
        yield return new WaitForSeconds(waitTime);
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
