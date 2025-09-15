// TapTwoPlayer.cs
using UnityEngine;
using UnityEngine.InputSystem;

public class TapTwoPlayer : MonoBehaviour
{
    public static TapTwoPlayer Instance { get; private set; }

    public int targetTapCount = 20; // 規定回数
    [SerializeField] private float limitTime = 10f;

    // キャラクター Transform
    [SerializeField] private Transform p1Character;
    [SerializeField] private Transform p2Character;
    [SerializeField] private Transform centerPoint;

    private Vector3 p1StartPos;
    private Vector3 p2StartPos;

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

        if (p1Character != null) p1StartPos = p1Character.position;
        if (p2Character != null) p2StartPos = p2Character.position;

        // StartCountdown開始
        if (GameUIController.Instance != null)
            StartCoroutine(GameUIController.Instance.StartCountdown());
        else
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
        GameUIController.Instance?.UpdateTimer(elapsed);

        // キャラクター移動
        UpdateCharacterPositions();

        // 勝利条件チェック
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
            _isRunning = false;
            GameUIController.Instance?.ShowTimeUp();
            FinishGame(0, elapsed); // 引き分け
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
        GameUIController.Instance?.UpdateTapCount(p1Remaining, p2Remaining);
    }

    private void UpdateCharacterPositions()
    {
        if (p1Character != null && centerPoint != null)
        {
            float progress1 = Mathf.Clamp01(_player1Count / (float)targetTapCount);
            p1Character.position = Vector3.Lerp(p1StartPos, centerPoint.position, progress1);
        }
        if (p2Character != null && centerPoint != null)
        {
            float progress2 = Mathf.Clamp01(_player2Count / (float)targetTapCount);
            p2Character.position = Vector3.Lerp(p2StartPos, centerPoint.position, progress2);
        }
    }

    private void FinishGame(int winner, float time)
    {
        if (!_isRunning) return;
        _isRunning = false;

        GameResultManager.Instance.SetResult(winner, _player1Count, _player2Count, time);
    }
}
