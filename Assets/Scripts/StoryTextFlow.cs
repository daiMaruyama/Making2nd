using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StoryTextFlow : MonoBehaviour
{
    [SerializeField] private TMP_Text _storyText;
    [SerializeField] private string _nextSceneName;

    [Header("�t�F�[�h�C������p�l��")]
    [SerializeField] private GameObject _panelToFade;
    [SerializeField] private float _fadeDuration = 1f;

    [Header("���ʉ�")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _charSound;

    [Header("���֎O�p�}�[�N")]
    [SerializeField] private TMP_Text _nextArrow;
    [SerializeField] private float _blinkInterval = 0.5f;
    private Coroutine _blinkCoroutine;

    private SpriteRenderer _panelSpriteRenderer;

    private string[] _storyLines =
    {
        "���ē����҂������D�P�ƕF���B",
        "������́c�c�܂����̃j�[�g���I",
        "�{�����V���2�l��V�̐�̗��݂ցB",
        "��������������A�N�Ɉ�x�����ĉ���������B",
        "���N�A�܂̍ĉ���ʂ���2�l�B",
        "�c�c�������N�͂ЂƖ��Ⴄ�I",
        "�V�̐�ɂ����鋴�̐^�񒆂�",
        "��ɒ������������A�{���ɑz���������I�H",
        "�����H �w�͂��H �͂��܂��������I�H",
        "�����A�����I"
    };

    private int _currentLine = 0;
    private bool _isTyping = false;

    void Start()
    {
        if (_panelToFade != null)
        {
            _panelSpriteRenderer = _panelToFade.GetComponentInChildren<SpriteRenderer>();
            if (_panelSpriteRenderer != null)
            {
                Color c = _panelSpriteRenderer.color;
                c.a = 0f;
                _panelSpriteRenderer.color = c;
            }
        }

        if (_nextArrow != null)
            _nextArrow.gameObject.SetActive(false);

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
        if (_isTyping)
        {
            StopAllCoroutines();
            _storyText.text = _storyLines[_currentLine];
            _isTyping = false;

            ShowNextArrow();
        }
        else
        {
            HideNextArrow();

            _currentLine++;
            if (_currentLine < _storyLines.Length)
            {
                ShowLine();

                if (_storyLines[_currentLine - 1] == "��������������A�N�Ɉ�x�����ĉ���������B")
                {
                    if (_panelSpriteRenderer != null)
                        StartCoroutine(FadeInSprite(_panelSpriteRenderer, _fadeDuration));
                }
            }
            else
            {
                SceneManager.LoadScene(_nextSceneName);
            }
        }
    }

    void ShowLine()
    {
        _storyText.text = "";
        StartCoroutine(TypeText(_storyLines[_currentLine], 0.05f));
    }

    private IEnumerator TypeText(string text, float delay)
    {
        _isTyping = true;
        _storyText.text = "";

        // �\���J�n��ɕ������艹�Đ�
        if (_audioSource != null && _charSound != null)
        {
            _audioSource.clip = _charSound;
            _audioSource.Play();
        }

        foreach (char c in text)
        {
            _storyText.text += c;
            yield return new WaitForSeconds(delay);
        }

        // �t���[�Y�\�������ŉ���~
        if (_audioSource != null)
            _audioSource.Stop();

        _isTyping = false;

        ShowNextArrow();
    }

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

        c.a = 0.5f;
        sprite.color = c;
    }

    // ------------------- TMP�O�p�}�[�N �_�� -------------------
    private void ShowNextArrow()
    {
        if (_nextArrow != null)
        {
            _nextArrow.gameObject.SetActive(true);

            if (_blinkCoroutine != null)
                StopCoroutine(_blinkCoroutine);

            _blinkCoroutine = StartCoroutine(BlinkArrow(_nextArrow, _blinkInterval));
        }
    }

    private void HideNextArrow()
    {
        if (_nextArrow != null)
        {
            _nextArrow.gameObject.SetActive(false);

            if (_blinkCoroutine != null)
            {
                StopCoroutine(_blinkCoroutine);
                _blinkCoroutine = null;
            }
        }
    }

    private IEnumerator BlinkArrow(TMP_Text arrow, float interval)
    {
        while (true)
        {
            arrow.enabled = !arrow.enabled;
            yield return new WaitForSeconds(interval);
        }
    }
}
