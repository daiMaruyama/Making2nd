using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StoryTextFlow : MonoBehaviour
{
    [SerializeField] private TMP_Text storyText;
    [SerializeField] private string nextSceneName;
    [Header("フェードインするパネル")]
    [SerializeField] private GameObject panelToFade; // パネルオブジェクト
    [SerializeField] private float fadeDuration = 1f;

    private SpriteRenderer _panelSpriteRenderer;

    private string[] storyLines =
    {
        "かつて働き者だった織姫と彦星。",
        "結婚後は……まさかのニート化！",

        "怒った天帝は2人を天の川の両岸へ。",
        "しかし情けをかけ、年に一度だけ再会を許した。",

        "毎年、涙の再会を果たす2人。",
        "……だが今年はひと味違う！",

        "天の川の真ん中に",
        "先に着いた方こそ、本当に想いが強い！？",

        "愛か？ 努力か？ はたまた根性か！？",
        "いざ、勝負！"
    };

    private int currentLine = 0;
    private bool isTyping = false;

    void Start()
    {
        // フェード対象のSpriteRenderer取得
        if (panelToFade != null)
        {
            _panelSpriteRenderer = panelToFade.GetComponentInChildren<SpriteRenderer>();
            if (_panelSpriteRenderer != null)
            {
                Color c = _panelSpriteRenderer.color;
                c.a = 0f; // 最初は透明
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

                // 指定セリフのあとにパネルフェードイン
                if (storyLines[currentLine - 1] == "しかし情けをかけ、年に一度だけ再会を許した。")
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

    // SpriteRendererを徐々に不透明にするCoroutine
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

        c.a = 0.5f; // 半透明
        sprite.color = c;
    }
}
