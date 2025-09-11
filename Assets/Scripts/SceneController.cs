using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;        // Fade用
using System.Collections;

public class SceneController : MonoBehaviour
{
    [Header("フェード設定")]
    [SerializeField] private Image fadePanel;  // 黒フェード用のImage
    [SerializeField] private float fadeTime = 1.0f;

    [Header("遷移先設定")]
    [SerializeField] private string nextSceneName;
    [SerializeField] private float waitBeforeFade = 0.5f; // 押されてからフェード開始までの待機時間

    bool isTransitioning = false;

    /// <summary>
    /// 外部から呼ぶ（例：ボタンOnClick）
    /// </summary>
    public void StartSceneTransition()
    {
        if (!isTransitioning)
        {
            StartCoroutine(TransitionRoutine());
        }
    }

    private IEnumerator TransitionRoutine()
    {
        isTransitioning = true;

        // 遷移前の待機
        if (waitBeforeFade > 0)
            yield return new WaitForSeconds(waitBeforeFade);

        // フェード演出
        if (fadePanel != null)
        {
            fadePanel.gameObject.SetActive(true);
            Color c = fadePanel.color;
            c.a = 0f;
            fadePanel.color = c;

            float timer = 0f;
            while (timer < fadeTime)
            {
                timer += Time.deltaTime;
                float alpha = Mathf.Clamp01(timer / fadeTime);
                fadePanel.color = new Color(c.r, c.g, c.b, alpha);
                yield return null;
            }
        }

        // シーン遷移
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
