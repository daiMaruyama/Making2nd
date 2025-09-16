using UnityEngine;
using TMPro;
using System.Collections;

public class GameUIController : MonoBehaviour
{
    public static GameUIController Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI countdownText;   // 中央演出
    [SerializeField] private TextMeshProUGUI timerText;       // 左上経過時間
    [SerializeField] private TextMeshProUGUI p1RemainingText; // P1残り回数
    [SerializeField] private TextMeshProUGUI p2RemainingText; // P2残り回数

    private void Awake()
    {
        Instance = this;
        ClearCountdown();
        UpdateTimer(0);
        timerText.gameObject.SetActive(false);
    }

    public IEnumerator StartCountdown(float waitBeforeStart = 1f)
    {
        yield return new WaitForSeconds(waitBeforeStart);

        int count = 3;
        while (count > 0)
        {
            countdownText.text = count.ToString();
            yield return new WaitForSeconds(1f);
            count--;
        }

        countdownText.text = "Start!";
        yield return new WaitForSeconds(0.2f);
        ClearCountdown();
        timerText.gameObject.SetActive(true);

        // タップ残数とタイマーを表示
        if (TapTwoPlayer.Instance != null)
        {
            UpdateTapCount(TapTwoPlayer.Instance.targetTapCount, TapTwoPlayer.Instance.targetTapCount);
            UpdateTimer(0f);
            TapTwoPlayer.Instance.StartGame();
        }
    }

    public void UpdateTimer(float elapsedTime)
    {
        if (timerText != null)
            timerText.text = $"Time: {elapsedTime:F2}s";
    }

    public void ShowFinishText(string message)
    {
        if (countdownText != null)
            countdownText.text = message;
    }

    public void ClearCountdown()
    {
        if (countdownText != null)
            countdownText.text = "";
        timerText.gameObject.SetActive(false);
    }

    public void UpdateTapCount(int p1Remaining, int p2Remaining)
    {
        if (p1RemainingText != null)
        {
            p1RemainingText.text = $"P1: {p1Remaining}";
            p1RemainingText.color = p1Remaining <= 0 ? Color.gray :
                                    p1Remaining <= TapTwoPlayer.Instance.targetTapCount / 5 ? Color.red :
                                    p1Remaining <= TapTwoPlayer.Instance.targetTapCount / 2 ? Color.yellow : Color.white;
        }

        if (p2RemainingText != null)
        {
            p2RemainingText.text = $"P2: {p2Remaining}";
            p2RemainingText.color = p2Remaining <= 0 ? Color.gray :
                                    p2Remaining <= TapTwoPlayer.Instance.targetTapCount / 5 ? Color.red :
                                    p2Remaining <= TapTwoPlayer.Instance.targetTapCount / 2 ? Color.yellow : Color.white;
        }
    }
}
