using UnityEngine;
using TMPro;
using System.Collections;

public class GameUIController : MonoBehaviour
{
    public static GameUIController Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI countdownText; // 中央演出（3,2,1,Start!, TimeUp!）
    [SerializeField] private TextMeshProUGUI timerText;     // 左上経過時間
    [SerializeField] private TextMeshProUGUI p1RemainingText; // P1残り回数
    [SerializeField] private TextMeshProUGUI p2RemainingText; // P2残り回数

    private void Awake()
    {
        Instance = this;
        ClearCountdown();
        UpdateTimer(0);
        ClearTapCount(); // 最初は非表示
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

        // Start! 表示
        countdownText.text = "Start!";
        yield return new WaitForSeconds(1f);

        // Start! が出たあとに残りタップ回数を初期化して表示
        if (TapTwoPlayer.Instance != null)
        {
            UpdateTapCount(TapTwoPlayer.Instance.targetTapCount, TapTwoPlayer.Instance.targetTapCount);
            TapTwoPlayer.Instance.StartGame();
        }

        ClearCountdown();
    }

    public void UpdateTimer(float elapsedTime)
    {
        if (timerText != null)
            timerText.text = $"Time: {elapsedTime:F2}s";
    }

    public void ShowTimeUp()
    {
        if (countdownText != null)
            countdownText.text = "Time Up!";
    }

    public void ClearCountdown()
    {
        if (countdownText != null)
            countdownText.text = "";
    }

    public void UpdateTapCount(int p1Remaining, int p2Remaining)
    {
        if (p1RemainingText != null)
        {
            p1RemainingText.text = $"P1: {p1Remaining}";
            if (p1Remaining <= 0) p1RemainingText.color = Color.gray;
            else if (p1Remaining <= TapTwoPlayer.Instance.targetTapCount / 5) p1RemainingText.color = Color.red;
            else if (p1Remaining <= TapTwoPlayer.Instance.targetTapCount / 2) p1RemainingText.color = Color.yellow;
            else p1RemainingText.color = Color.white;
        }

        if (p2RemainingText != null)
        {
            p2RemainingText.text = $"P2: {p2Remaining}";
            if (p2Remaining <= 0) p2RemainingText.color = Color.gray;
            else if (p2Remaining <= TapTwoPlayer.Instance.targetTapCount / 5) p2RemainingText.color = Color.red;
            else if (p2Remaining <= TapTwoPlayer.Instance.targetTapCount / 2) p2RemainingText.color = Color.yellow;
            else p2RemainingText.color = Color.white;
        }
    }

    public void ClearTapCount()
    {
        if (p1RemainingText != null) p1RemainingText.text = "";
        if (p2RemainingText != null) p2RemainingText.text = "";
    }
}
