using UnityEngine;
using UnityEngine.SceneManagement;

public class GameResultManager : MonoBehaviour
{
    public static GameResultManager Instance { get; private set; }

    private int _winner;
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

    public void SetResult(int winner, int p1, int p2)
    {
        _winner = winner;
        _p1Count = p1;
        _p2Count = p2;

        // ランキング保存（勝者のスコアを保存）
        int scoreToSave = (winner == 1) ? _p1Count : (winner == 2 ? _p2Count : Mathf.Max(_p1Count, _p2Count));
        SaveRecord(scoreToSave);

        // Finish演出表示
        FinishUIController.Instance?.ShowFinish(winner);

        // 数秒後に遷移
        Invoke(nameof(GoToResultScene), delayBeforeSceneChange);
    }

    private void SaveRecord(int newScore)
    {
        const int MaxRank = 5;

        // 既存のランキングを取得
        int[] scores = new int[MaxRank];
        for (int i = 0; i < MaxRank; i++)
        {
            scores[i] = PlayerPrefs.GetInt($"BestScore{i}", 0);
        }

        // 新記録を挿入（降順：スコア大きいほど上）
        for (int i = 0; i < MaxRank; i++)
        {
            if (newScore > scores[i])
            {
                for (int j = MaxRank - 1; j > i; j--)
                {
                    scores[j] = scores[j - 1];
                }
                scores[i] = newScore;
                break;
            }
        }

        // 保存
        for (int i = 0; i < MaxRank; i++)
        {
            PlayerPrefs.SetInt($"BestScore{i}", scores[i]);
        }

        PlayerPrefs.Save();
    }

    private void GoToResultScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    // ResultScene で呼ぶ用
    public (int winner, int p1, int p2) GetResult()
    {
        return (_winner, _p1Count, _p2Count);
    }
}
