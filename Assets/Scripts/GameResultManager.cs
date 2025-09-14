using UnityEngine;
using UnityEngine.SceneManagement;

public class GameResultManager : MonoBehaviour
{
    public static GameResultManager Instance { get; private set; }

    private int _winner;
    private float _time;
    private int _p1Count;
    private int _p2Count;

    [SerializeField] private float delayBeforeSceneChange = 3f; // シリアライズで設定
    [SerializeField] string sceneName;

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

    public void SetResult(int winner, float time, int p1, int p2)
    {
        _winner = winner;
        _time = time;
        _p1Count = p1;
        _p2Count = p2;

        // ランキング保存
        SaveRecord(time);

        // Finish演出表示
        FinishUIController.Instance?.ShowFinish(winner);

        // 数秒後に遷移
        Invoke(nameof(GoToResultScene), delayBeforeSceneChange);
    }

    private void SaveRecord(float newTime)
    {
        const int MaxRank = 5;

        // 既存のランキングを取得
        float[] times = new float[MaxRank];
        for (int i = 0; i < MaxRank; i++)
        {
            times[i] = PlayerPrefs.GetFloat($"BestTime{i}", float.MaxValue);
        }

        // 新記録を挿入
        for (int i = 0; i < MaxRank; i++)
        {
            if (newTime < times[i])
            {
                for (int j = MaxRank - 1; j > i; j--)
                {
                    times[j] = times[j - 1];
                }
                times[i] = newTime;
                break;
            }
        }

        // 保存
        for (int i = 0; i < MaxRank; i++)
        {
            PlayerPrefs.SetFloat($"BestTime{i}", times[i]);
        }

        PlayerPrefs.Save();
    }

    private void GoToResultScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    // ResultScene で呼ぶ用
    public (int winner, float time, int p1, int p2) GetResult()
    {
        return (_winner, _time, _p1Count, _p2Count);
    }
}
