// GameResultManager.cs
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameResultManager : MonoBehaviour
{
    public static GameResultManager Instance { get; private set; }

    private int _winner;
    private int _p1Count;
    private int _p2Count;
    private float _clearTime;

    [Header("シーン & リロード設定")]
    [SerializeField] private float delayBeforeReload = 3f;
    [SerializeField] private string gameSceneName = "Ingame"; // ゲームシーン名

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

        // ランキング判定など
        if (winner == 1 || winner == 2)
            SaveRecord(time);

        // Finish後 n 秒でゲーム本編をリロード
        Invoke(nameof(ReloadGameScene), delayBeforeReload);
    }

    private void SaveRecord(float newTime)
    {
        const int MaxRank = 5;

        float[] times = new float[MaxRank];
        for (int i = 0; i < MaxRank; i++)
            times[i] = PlayerPrefs.GetFloat($"BestTime{i}", float.MaxValue);

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
            for (int j = MaxRank - 1; j > insertIndex; j--)
                times[j] = times[j - 1];

            times[insertIndex] = newTime;

            for (int i = 0; i < MaxRank; i++)
                PlayerPrefs.SetFloat($"BestTime{i}", times[i]);

            PlayerPrefs.Save();
        }
    }

    private void ReloadGameScene()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    // 結果を取得するためのGetter
    public (int winner, int p1, int p2, float time) GetResult() => (_winner, _p1Count, _p2Count, _clearTime);
}
