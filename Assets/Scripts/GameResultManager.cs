using UnityEngine;
using UnityEngine.SceneManagement;

public class GameResultManager : MonoBehaviour
{
    public static GameResultManager Instance { get; private set; }

    private int _winner;
    private int _p1Count;
    private int _p2Count;
    private float _clearTime;

    [SerializeField] private float delayBeforeSceneChange = 3f;
    [SerializeField] private string sceneName;

    // ランキング更新フラグ（ResultSceneが参照）
    private bool _lastWasNewRecord = false;
    private int _lastNewRecordIndex = -1;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// winner: 0 = draw, 1 = P1, 2 = P2
    /// </summary>
    public void SetResult(int winner, int p1, int p2, float time)
    {
        _winner = winner;
        _p1Count = p1;
        _p2Count = p2;
        _clearTime = time;

        // Reset flags
        _lastWasNewRecord = false;
        _lastNewRecordIndex = -1;

        // 勝者がいる場合のみランキング判定（速さが指標）
        if (winner == 1 || winner == 2)
        {
            SaveRecord(time);
        }

        // ※ インゲームでは勝者演出は行わない（ResultScene で表示するため）
        // ResultScene へ遷移
        Invoke(nameof(GoToResultScene), delayBeforeSceneChange);
    }

    private void SaveRecord(float newTime)
    {
        const int MaxRank = 5;

        float[] times = new float[MaxRank];
        for (int i = 0; i < MaxRank; i++)
        {
            times[i] = PlayerPrefs.GetFloat($"BestTime{i}", float.MaxValue);
        }

        // 挿入位置を探す（小さいタイムが上位）
        int insertIndex = -1;
        for (int i = 0; i < MaxRank; i++)
        {
            if (newTime < times[i])
            {
                insertIndex = i;
                break;
            }
        }

        if (insertIndex != -1)
        {
            // 挿入して後ろをシフト
            for (int j = MaxRank - 1; j > insertIndex; j--)
            {
                times[j] = times[j - 1];
            }
            times[insertIndex] = newTime;

            // 保存
            for (int i = 0; i < MaxRank; i++)
            {
                PlayerPrefs.SetFloat($"BestTime{i}", times[i]);
            }
            PlayerPrefs.Save();

            // フラグをセット（ResultScene で名前入力などを行うため）
            _lastWasNewRecord = true;
            _lastNewRecordIndex = insertIndex;
        }
        else
        {
            // 新記録に入らなかった場合は何もしない（既存のランキング保持）
            _lastWasNewRecord = false;
            _lastNewRecordIndex = -1;
        }
    }

    private void GoToResultScene()
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogWarning("GameResultManager: sceneName is not set.");
            return;
        }
        SceneManager.LoadScene(sceneName);
    }

    // ResultScene から呼べる getter
    public (int winner, int p1, int p2, float time) GetResult()
    {
        return (_winner, _p1Count, _p2Count, _clearTime);
    }

    // ResultScene が参照して「名前入力が必要か」を確認するためのAPI
    public bool LastResultWasNewRecord() => _lastWasNewRecord;
    public int GetLastNewRecordIndex() => _lastNewRecordIndex;
}
