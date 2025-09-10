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
        "天の川を隔てて会えない織姫と彦星。",
        "年に一度だけ天の川が消滅する。",
        "彼らは猛スピードで会いに行くが……",
        "どちらの想いが強いのか！？"
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
