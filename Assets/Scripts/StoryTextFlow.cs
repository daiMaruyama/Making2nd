using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem; // �� �VInputSystem���g��
using UnityEngine.SceneManagement;

public class StoryTextFlow : MonoBehaviour
{
    [SerializeField] private TMP_Text storyText;
    [SerializeField] private string nextSceneName;

    private string[] storyLines =
{
    "���ē����҂������D�P�ƕF���B",
    "������́c�c�܂����̃j�[�g���I",

    "�{�����V���2�l��V�̐�̗��݂ցB",
    "��������������A�N�Ɉ�x�����ĉ���������B",

    "���N�A�܂̍ĉ���ʂ���2�l�B",
    "�c�c�������N�͂ЂƖ��Ⴄ�I",

    "�V�̐�̐^�񒆂�",
    "��ɒ������������A�{���ɑz���������I�H",

    "�����H �w�͂��H �͂��܂��������I�H",
    "�����A�����I"
};

    private int currentLine = 0;
    private bool isTyping = false;

    void Start()
    {
        ShowLine();
    }

    void Update()
    {
        // �VInputSystem�́u�C�ӂ̃{�^�� or �^�b�v���́v���Ď�
        if (Keyboard.current.spaceKey.wasPressedThisFrame ||
            Mouse.current.leftButton.wasPressedThisFrame ||
            Touchscreen.current?.primaryTouch.press.wasPressedThisFrame == true)
        {
            OnTap();
        }
    }

    private void OnTap()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            storyText.text = storyLines[currentLine];
            isTyping = false;
        }
        else
        {
            currentLine++;
            if (currentLine < storyLines.Length)
            {
                ShowLine();
            }
            else
            {
                Debug.Log("�X�g�[���[�I�� �� ���̃V�[���֑J�ڂƂ�");
                SceneManager.LoadScene(nextSceneName);
            }
        }
    }

    void ShowLine()
    {
        storyText.text = "";
        StartCoroutine(TypeText(storyLines[currentLine], 0.05f));
    }

    IEnumerator TypeText(string text, float delay)
    {
        isTyping = true;
        storyText.text = "";

        foreach (char c in text)
        {
            storyText.text += c;
            yield return new WaitForSeconds(delay);
        }

        isTyping = false;
    }
}
