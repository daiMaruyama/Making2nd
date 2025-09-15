using UnityEngine;
using UnityEngine.InputSystem;

public class TapTwoPlayer : MonoBehaviour
{
    public static TapTwoPlayer Instance { get; private set; }

    [SerializeField] private float _limitTime = 5f; // ��������

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

        // �Q�[���J�n�O�J�E���g�_�E��
        StartCoroutine(GameUIController.Instance.StartCountdown());
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
        HandleMobileInput();

        float elapsed = Time.time - _startTime;
        float remaining = Mathf.Max(0, _limitTime - elapsed);

        // �E��ɏ펞�\��
        GameUIController.Instance.UpdateTimer(remaining);

        // �����ɃJ�E���g���o (5,4,3...)
        if (remaining > 0)
        {
            GameUIController.Instance.UpdateTimer(remaining);
        }
        else
        {
            _isRunning = false;

            // �^�񒆂� TimeUp! ���o��
            GameUIController.Instance.ShowTimeUp();

            int winner = (_player1Count == _player2Count) ? 0 :
                         (_player1Count > _player2Count ? 1 : 2);

            FinishGame(winner);
        }
    }

    private void HandlePCInput()
    {
        if (Keyboard.current == null) return;

        // Player1: A/D
        if (Keyboard.current.aKey.wasPressedThisFrame || Keyboard.current.dKey.wasPressedThisFrame)
        {
            _player1Count++;
            Debug.Log($"<color=red>P1 {_player1Count}��</color>");
        }

        // Player2: J/L
        if (Keyboard.current.jKey.wasPressedThisFrame || Keyboard.current.lKey.wasPressedThisFrame)
        {
            _player2Count++;
            Debug.Log($"<color=blue>P2 {_player2Count}��</color>");
        }
    }

    private void HandleMobileInput()
    {
        if (Touchscreen.current == null) return;

        if (Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            Vector2 pos = Touchscreen.current.primaryTouch.position.ReadValue();

            if (pos.x < Screen.width / 2)
            {
                _player1Count++;
                Debug.Log($"<color=red>P1 {_player1Count}��</color>");
            }
            else
            {
                _player2Count++;
                Debug.Log($"<color=blue>P2 {_player2Count}��</color>");
            }
        }
    }

    private void FinishGame(int winner)
    {
        GameResultManager.Instance.SetResult(winner, _player1Count, _player2Count);
    }
}
