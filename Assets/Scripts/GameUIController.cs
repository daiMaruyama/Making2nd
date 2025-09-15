using UnityEngine;
using TMPro;
using System.Collections;

public class GameUIController : MonoBehaviour
{
    public static GameUIController Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI countdownText; // 中央の演出 (3,2,1,Start / 5,4,3,2,1,TimeUp)
    [SerializeField] private TextMeshProUGUI timerText;     // 右上の常時残り時間

    private void Awake()
    {
        Instance = this;
        if (countdownText != null) countdownText.text = "";
        if (timerText != null) timerText.text = "";
    }

    /// <summary>
    /// ゲーム開始前カウントダウン
    /// </summary>
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
        yield return new WaitForSeconds(1f);
        countdownText.text = "";

        TapTwoPlayer.Instance.StartGame();
    }

    /// <summary>
    /// プレイ中の残り時間を右上に表示
    /// </summary>
    public void UpdateTimer(float timeLeft)
    {
        timerText.text = $"Time: {Mathf.Ceil(timeLeft)}";
    }

    /// <summary>
    /// 真ん中に「TimeUp!」を表示
    /// </summary>
    public void ShowTimeUp()
    {
        countdownText.text = "Time Up!";
    }

    public void ClearCountdown()
    {
        countdownText.text = "";
    }
}
