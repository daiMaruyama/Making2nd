using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;  // 新Input System
using System.Collections;

public class PressToScreen : MonoBehaviour
{
    [Header("シーン遷移設定")]
    [SerializeField] private string nextSceneName;
    [SerializeField] private float waitTime = 1.0f;

    bool isPressed = false;
    bool isTransitioning = false; // 二重呼び防止フラグ

    void Update()
    {
        if (isTransitioning || isPressed) return;

        // --- 画面タップ or クリック ---
        if ((Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame) ||
            (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame))
        {
            isPressed = true;
            StartCoroutine(SceneTransition());
        }
    }

    IEnumerator SceneTransition()
    {
        isTransitioning = true;
        yield return new WaitForSeconds(waitTime);

        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
