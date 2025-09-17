using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test2 : MonoBehaviour
{
    [SerializeField] private TutorialTextController tutorial;
    [SerializeField] private float waitTime = 1f;
    [SerializeField, TextArea] private string[] messages; // �������b�Z�[�W
    private int currentIndex = 0;

    private void Start()
    {
        StartCoroutine(ShowDelay());
    }

    private IEnumerator ShowDelay()
    {
        yield return new WaitForSeconds(waitTime);
        ShowNextMessage();
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame ||
            Touchscreen.current?.primaryTouch.press.wasPressedThisFrame == true)
        {
            ShowNextMessage();
        }
    }

    private void ShowNextMessage()
    {
        if (currentIndex < messages.Length)
        {
            tutorial.ShowMessage(messages[currentIndex]);
            currentIndex++;
        }
        else
        {
            tutorial.HideMessage(); // �S���I��������\��
        }
    }
}
