using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test : MonoBehaviour
{
    [SerializeField] private TutorialTextController tutorial;
    [SerializeField] private float waitTime = 1f;
    [SerializeField, TextArea] private string message = "‰æ–Ê‚ðƒ^ƒbƒv‚µ‚Ä‚­‚¾‚³‚¢";

    private void Start()
    {
        StartCoroutine(ShowDelay());
    }

    private IEnumerator ShowDelay()
    {
        yield return new WaitForSeconds(waitTime);
        tutorial.ShowMessage(message);
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame ||
            Touchscreen.current?.primaryTouch.press.wasPressedThisFrame == true)
        {
            tutorial.HideMessage();
        }
    }
}
