using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem; // ← 新InputSystemを使う
using UnityEngine.SceneManagement;

public class StoryTextFlow : MonoBehaviour
{
    [SerializeField] private TMP_Text storyText;
    [SerializeField] private string nextSceneName;

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
        ShowLine();
    }

    void Update()
    {
        // 新InputSystemの「任意のボタン or タップ入力」を監視
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
                Debug.Log("ストーリー終了 → 次のシーンへ遷移とか");
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
