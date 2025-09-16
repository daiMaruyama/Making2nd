using UnityEngine;
using UnityEngine.InputSystem;

public class TapTwoPlayer : MonoBehaviour
{
    public static TapTwoPlayer Instance { get; private set; }

    [Header("�Q�[���ݒ�")]
    public int targetTapCount = 20;
    [SerializeField] private float limitTime = 10f;

    [Header("�L�����N�^�[")]
    [SerializeField] private Transform p1Character;
    [SerializeField] private Transform p2Character;
    [SerializeField] private Transform centerPoint;

    public bool IsRunning => _isRunning;
    public int Player2Count => _player2Count; // �� public getter ��ǉ�
    public bool IsAI => _isAI;               // �� AI���[�h�����肷��getter


    private Vector3 p1StartPos;
    private Vector3 p2StartPos;

    private int _player1Count = 0;
    private int _player2Count = 0;

    private float _startTime;
    private bool _isRunning = false;

    // ===========================
    // AI�ݒ�
    // ===========================
    private static bool _isAI = false;
    private static bool _isOniMode = false;
    private float _nextAITapTime = 0f;

    public static void SetAIParameters(bool isAI, bool isOniMode)
    {
        _isAI = isAI;
        _isOniMode = isOniMode;
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // --- PlayerPrefs ����Q�[�����[�h���擾 ---
        int mode = PlayerPrefs.GetInt("GameMode", 3); // �f�t�H���g2�l
        if (mode == 1) SetAIParameters(true, false);   // 1�l�V���v��
        else if (mode == 2) SetAIParameters(true, true); // 1�l�S
        else SetAIParameters(false, false);           // 2�l�ΐ�

        _player1Count = 0;
        _player2Count = 0;
        _isRunning = false;

        if (p1Character != null) p1StartPos = p1Character.position;
        if (p2Character != null) p2StartPos = p2Character.position;

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

        if (_isAI)
            HandleAIInput();

        float elapsed = Time.time - _startTime;
        GameUIController.Instance?.UpdateTimer(elapsed);

        UpdateCharacterPositions();

        // ���s����
        if (_player1Count >= targetTapCount)
        {
            GameUIController.Instance?.ShowFinishText("FINISH!");
            FinishGame(1, elapsed);
        }
        else if (_player2Count >= targetTapCount)
        {
            GameUIController.Instance?.ShowFinishText("FINISH!");
            FinishGame(2, elapsed);
        }
        else if (elapsed >= limitTime)
        {
            _isRunning = false;
            GameUIController.Instance?.ShowFinishText("TIME UP!");
            FinishGame(0, elapsed); // ��������
        }
    }

    private void HandlePCInput()
    {
        if (Keyboard.current == null) return;

        // Player2���͂�AI�łȂ���ΗL��
        if (!_isAI)
        {
            if (Keyboard.current.jKey.wasPressedThisFrame || Keyboard.current.lKey.wasPressedThisFrame)
            {
                _player2Count++;
                UpdateTapUI();
            }
        }

        // Player1
        if (Keyboard.current.aKey.wasPressedThisFrame || Keyboard.current.dKey.wasPressedThisFrame)
        {
            _player1Count++;
            UpdateTapUI();
        }
    }

    private void HandleAIInput()
    {
        if (Time.time >= _nextAITapTime)
        {
            _player2Count++;
            UpdateTapUI();

            if (_isOniMode)
            {
                float bestTime = PlayerPrefs.GetFloat("BestTime0", 2f);
                _nextAITapTime = Time.time + bestTime / targetTapCount;
            }
            else
            {
                _nextAITapTime = Time.time + Random.Range(0.1f, 0.3f);
            }
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
