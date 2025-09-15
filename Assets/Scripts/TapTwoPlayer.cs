using UnityEngine;
using UnityEngine.InputSystem;

public class TapTwoPlayer : MonoBehaviour
{
    public static TapTwoPlayer Instance { get; private set; }

    public int targetTapCount = 20; // 規定回数
    [SerializeField] private float limitTime = 10f;   // 制限時間

    private int _player1Count = 0;
    private int _player2Count = 0;

    private float _startTime;
    private bool _isRunning = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _player1Count = 0;
        _player2Count = 0;
        _isRunning = false;

        // 最初は残り回数非表示
        // GameUIController.Instance?.UpdateTapCount(targetTapCount, targetTapCount); ←削除

        // カウントダウン開始
        if (GameUIController.Instance != null)
            StartCoroutine(GameUIController.Instance.StartCountdown());
        else
            StartGame(); // UIがない場合は直接開始
    }

    // GameUIController がない場合のフォールバック
    private System.Collections.IEnumerator DummyCountdown()
    {
        yield return null;
        StartGame();
    }

    public void StartGame()
    {
        _startTime = Time.time;
        _isRunning = true;
    }

    private void Update()
    {
        if (!_isRunning) return;

        HandlePCInput();

        float elapsed = Time.time - _startTime;

        // 左上タイマー（0.00s形式）
        GameUIController.Instance?.UpdateTimer(elapsed);

        // 勝利条件チェック（先に規定回数に達した方が勝ち）
        if (_player1Count >= targetTapCount)
        {
            FinishGame(1, elapsed);
        }
        else if (_player2Count >= targetTapCount)
        {
            FinishGame(2, elapsed);
        }
        else if (elapsed >= limitTime)
        {
            // 時間切れ。インゲームでは TimeUp 表示だけ（勝者表示はしない）
            _isRunning = false;
            GameUIController.Instance?.ShowTimeUp();
            FinishGame(0, elapsed); // 引き分け扱い
        }
    }

    private void HandlePCInput()
    {
        if (Keyboard.current == null) return;

        if (Keyboard.current.aKey.wasPressedThisFrame || Keyboard.current.dKey.wasPressedThisFrame)
        {
            _player1Count++;
            UpdateTapUI();
        }

        if (Keyboard.current.jKey.wasPressedThisFrame || Keyboard.current.lKey.wasPressedThisFrame)
        {
            _player2Count++;
            UpdateTapUI();
        }
    }


    private void UpdateTapUI()
    {
        int p1Remaining = Mathf.Max(0, targetTapCount - _player1Count);
        int p2Remaining = Mathf.Max(0, targetTapCount - _player2Count);

        GameUIController.Instance.UpdateTapCount(p1Remaining, p2Remaining);
    }


    private void FinishGame(int winner, float time)
    {
        if (!_isRunning) return; // 二重呼び出し防止
        _isRunning = false;

        // 結果を渡す（勝者の到達タイムのみがランキング対象）
        GameResultManager.Instance.SetResult(winner, _player1Count, _player2Count, time);
    }
}
