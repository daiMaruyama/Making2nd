using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StoryTextFlow : MonoBehaviour
{
    [SerializeField] private TMP_Text _storyText;
    [SerializeField] private string _nextSceneName;

    [Header("フェードインするパネル")]
    [SerializeField] private GameObject _panelToFade;
    [SerializeField] private float _fadeDuration = 1f;

    [Header("効果音")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _charSound;

    [Header("次へ三角マーク")]
    [SerializeField] private TMP_Text _nextArrow;
    [SerializeField] private float _blinkInterval = 0.5f;
    private Coroutine _blinkCoroutine;

    private SpriteRenderer _panelSpriteRenderer;

    private string[] _storyLines =
    {
        "かつて働き者だった織姫と彦星。",
        "結婚後は……まさかのニート化！",
        "怒った天帝は2人を天の川の両岸へ。",
        "しかし情けをかけ、年に一度だけ再会を許した。",
        "毎年、涙の再会を果たす2人。",
        "……だが今年はひと味違う！",
        "天の川にかかる橋の真ん中に",
        "先に着いた方こそ、本当に想いが強い！？",
        "愛か？ 努力か？ はたまた根性か！？",
        "いざ、勝負！"
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

                if (_storyLines[_currentLine - 1] == "しかし情けをかけ、年に一度だけ再会を許した。")
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

        // 表示開始後に文字送り音再生
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

        // フレーズ表示完了で音停止
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

    // ------------------- TMP三角マーク 点滅 -------------------
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
