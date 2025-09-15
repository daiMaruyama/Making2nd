using UnityEngine;
using TMPro;
using System.Collections;

public class GameUIController : MonoBehaviour
{
    public static GameUIController Instance { get; private set; }

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI countdownText;     // 中央演出
    [SerializeField] private TextMeshProUGUI timerText;         // 左上タイマー
    [SerializeField] private TextMeshProUGUI p1RemainingText;   // P1残り回数
    [SerializeField] private TextMeshProUGUI p2RemainingText;   // P2残り回数

    [Header("Skybox")]
    [SerializeField] private Material startSkybox;
    [SerializeField] private Material defaultSkybox;

    private void Awake()
    {
        Instance = this;

        // Canvas は必ずアクティブ、countdownText もアクティブにして透明に
        if (countdownText != null)
        {
            countdownText.gameObject.SetActive(true);
            var c = countdownText.color;
            c.a = 0f;
            countdownText.color = c;
        }

        HideTimer();
        HideTapCount();

        if (defaultSkybox != null)
            RenderSettings.skybox = defaultSkybox;
    }

    public IEnumerator StartCountdown(float waitBeforeStart = 1f)
    {
        yield return new WaitForSeconds(waitBeforeStart);

        // カウントダウン表示
        for (int i = 3; i > 0; i--)
        {
            SetCountdownText(i.ToString());
            yield return new WaitForSeconds(1f);
        }

        // Start! 表示
        SetCountdownText("Start!");
        yield return new WaitForSeconds(1f);

        // Countdown 完了で Alpha を 0 に
        SetCountdownTextAlpha(0f);

        // Skybox 切替
        if (startSkybox != null)
            RenderSettings.skybox = startSkybox;

        // タップカウントとタイマー表示
        ShowTapCount();
        ShowTimer(0f);

        if (TapTwoPlayer.Instance != null)
        {
            UpdateTapCount(TapTwoPlayer.Instance.targetTapCount, TapTwoPlayer.Instance.targetTapCount);
            TapTwoPlayer.Instance.StartGame();
        }
    }

    #region Countdown
    private void SetCountdownText(string text)
    {
        if (countdownText != null)
        {
            countdownText.text = text;
            SetCountdownTextAlpha(1f);
        }
    }

    private void SetCountdownTextAlpha(float alpha)
    {
        if (countdownText != null)
        {
            Color c = countdownText.color;
            c.a = alpha;
            countdownText.color = c;
        }
    }
    #endregion

    #region Timer
    public void UpdateTimer(float elapsedTime)
    {
        if (timerText != null && timerText.gameObject.activeSelf)
            timerText.text = $"Time: {elapsedTime:F2}s";
    }

    private void ShowTimer(float elapsedTime)
    {
        if (timerText != null)
        {
            timerText.gameObject.SetActive(true);
            timerText.text = $"Time: {elapsedTime:F2}s";
        }
    }

    private void HideTimer()
    {
        if (timerText != null)
            timerText.gameObject.SetActive(false);
    }
    #endregion

    #region TapCount
    public void UpdateTapCount(int p1Remaining, int p2Remaining)
    {
        if (p1RemainingText != null)
        {
            p1RemainingText.text = $"{p1Remaining}";
            p1RemainingText.color = GetTapColor(p1Remaining);
        }

        if (p2RemainingText != null)
        {
            p2RemainingText.text = $"{p2Remaining}";
            p2RemainingText.color = GetTapColor(p2Remaining);
        }
    }

    private Color GetTapColor(int remaining)
    {
        int max = TapTwoPlayer.Instance.targetTapCount;
        if (remaining <= 0) return Color.gray;
        if (remaining <= max / 5) return Color.red;
        if (remaining <= max / 2) return Color.yellow;
        return Color.white;
    }

    private void ShowTapCount()
    {
        if (p1RemainingText != null && p1RemainingText.transform.parent != null)
            p1RemainingText.transform.parent.gameObject.SetActive(true);
        if (p2RemainingText != null && p2RemainingText.transform.parent != null)
            p2RemainingText.transform.parent.gameObject.SetActive(true);
    }

    private void HideTapCount()
    {
        if (p1RemainingText != null && p1RemainingText.transform.parent != null)
            p1RemainingText.transform.parent.gameObject.SetActive(false);
        if (p2RemainingText != null && p2RemainingText.transform.parent != null)
            p2RemainingText.transform.parent.gameObject.SetActive(false);
    }
    #endregion

    public void ShowTimeUp()
    {
        SetCountdownText("Time Up!");
    }
}
