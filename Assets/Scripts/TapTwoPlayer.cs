using UnityEngine;
using UnityEngine.InputSystem;

public class TapTwoPlayer : MonoBehaviour
{
    public static TapTwoPlayer Instance { get; private set; }

    public int targetTapCount = 20; // �K���
    [SerializeField] private float limitTime = 10f;   // ��������

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

        // �ŏ��͎c��񐔔�\��
        // GameUIController.Instance?.UpdateTapCount(targetTapCount, targetTapCount); ���폜

        // �J�E���g�_�E���J�n
        if (GameUIController.Instance != null)
            StartCoroutine(GameUIController.Instance.StartCountdown());
        else
            StartGame(); // UI���Ȃ��ꍇ�͒��ڊJ�n
    }

    // GameUIController ���Ȃ��ꍇ�̃t�H�[���o�b�N
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

        // ����^�C�}�[�i0.00s�`���j
        GameUIController.Instance?.UpdateTimer(elapsed);

        // ���������`�F�b�N�i��ɋK��񐔂ɒB�������������j
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
            // ���Ԑ؂�B�C���Q�[���ł� TimeUp �\�������i���ҕ\���͂��Ȃ��j
            _isRunning = false;
            GameUIController.Instance?.ShowTimeUp();
            FinishGame(0, elapsed); // ������������
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
        if (!_isRunning) return; // ��d�Ăяo���h�~
        _isRunning = false;

        // ���ʂ�n���i���҂̓��B�^�C���݂̂������L���O�Ώہj
        GameResultManager.Instance.SetResult(winner, _player1Count, _player2Count, time);
    }
}
