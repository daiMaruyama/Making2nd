using UnityEngine;
using UnityEngine.SceneManagement;

public class GameResultManager : MonoBehaviour
{
    public static GameResultManager Instance { get; private set; }

    private int _winner;
    private int _p1Count;
    private int _p2Count;
    private float _clearTime;

    public static float LastTime;
    public static bool NeedsNameInput;

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

        LastTime = time;

        bool isSoloMode = TapTwoPlayer.Instance != null && TapTwoPlayer.Instance.IsAI;

        if (isSoloMode)
        {
            // ソロモード：勝者が1（プレイヤー1）の場合のみランキング更新
            NeedsNameInput = (winner == 1) && RankingSystem.IsNewRecord(time);
        }
        else
        {
            // 2人対戦：勝者が1か2でランキング更新
            NeedsNameInput = (winner == 1 || winner == 2) && RankingSystem.IsNewRecord(time);
        }

        // ゲームシーンを n 秒後にリロード
        Invoke(nameof(ReloadGameScene), delayBeforeReload);
    }


    private void ReloadGameScene()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    // 結果を取得するためのGetter
    public (int winner, int p1, int p2, float time) GetResult() => (_winner, _p1Count, _p2Count, _clearTime);
}
