using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StoryTextFlow : MonoBehaviour
{
    [SerializeField] private TMP_Text storyText;
    [SerializeField] private string nextSceneName;
    [Header("�t�F�[�h�C������p�l��")]
    [SerializeField] private GameObject panelToFade; // �p�l���I�u�W�F�N�g
    [SerializeField] private float fadeDuration = 1f;

    private SpriteRenderer _panelSpriteRenderer;

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
        // �t�F�[�h�Ώۂ�SpriteRenderer�擾
        if (panelToFade != null)
        {
            _panelSpriteRenderer = panelToFade.GetComponentInChildren<SpriteRenderer>();
            if (_panelSpriteRenderer != null)
            {
                Color c = _panelSpriteRenderer.color;
                c.a = 0f; // �ŏ��͓���
                _panelSpriteRenderer.color = c;
            }
        }

        ShowLine();
    }

    void Update()
    {
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

                // �w��Z���t�̂��ƂɃp�l���t�F�[�h�C��
                if (storyLines[currentLine - 1] == "��������������A�N�Ɉ�x�����ĉ���������B")
                {
                    if (_panelSpriteRenderer != null)
                        StartCoroutine(FadeInSprite(_panelSpriteRenderer, fadeDuration));
                }
            }
            else
            {
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

    // SpriteRenderer�����X�ɕs�����ɂ���Coroutine
    private IEnumerator FadeInSprite(SpriteRenderer sprite, float duration)
    {
        float elapsed = 0f;
        Color c = sprite.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            c.a = Mathf.Clamp01(elapsed / duration);
            sprite.color = c;
            yield return null;
        }

        c.a = 0.5f; // ������
        sprite.color = c;
    }
}
