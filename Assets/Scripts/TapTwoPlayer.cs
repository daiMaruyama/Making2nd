using UnityEngine;
using UnityEngine.InputSystem;

public class TapTwoPlayer : MonoBehaviour
{
    [SerializeField] private int _limitCount = 50; // �K���

    private int _player1Count = 0;
    private int _player2Count = 0;

    private float _startTime;
    private bool _isRunning = false;

    private void Start()
    {
        _player1Count = 0;
        _player2Count = 0;
        _isRunning = true;
        _startTime = Time.time;
    }

    private void Update()
    {
        if (!_isRunning) return;

        HandlePCInput();
        HandleMobileInput();

        // ���s����
        if (_player1Count >= _limitCount)
        {
            FinishGame(1);
        }
        else if (_player2Count >= _limitCount)
        {
            FinishGame(2);
        }
    }

    // PC����

    private void HandlePCInput()
    {
        // Player1 �L�[�Q
        if (Keyboard.current.qKey.wasPressedThisFrame ||
            Keyboard.current.aKey.wasPressedThisFrame ||
            Keyboard.current.zKey.wasPressedThisFrame ||
            Keyboard.current.wKey.wasPressedThisFrame ||
            Keyboard.current.sKey.wasPressedThisFrame ||
            Keyboard.current.xKey.wasPressedThisFrame ||
            Keyboard.current.eKey.wasPressedThisFrame ||
            Keyboard.current.dKey.wasPressedThisFrame ||
            Keyboard.current.cKey.wasPressedThisFrame)
        {
            _player1Count++;
        }

        // Player2 �L�[�Q
        if (Keyboard.current.iKey.wasPressedThisFrame ||
            Keyboard.current.kKey.wasPressedThisFrame ||
            Keyboard.current.oKey.wasPressedThisFrame ||
            Keyboard.current.lKey.wasPressedThisFrame ||
            Keyboard.current.pKey.wasPressedThisFrame ||
            Keyboard.current.semicolonKey.wasPressedThisFrame ||  // �u�{�H�v�L�[�����ˑ��Ȃ̂ŃZ�~�R�����ɉ��ݒ�
            Keyboard.current.commaKey.wasPressedThisFrame ||
            Keyboard.current.periodKey.wasPressedThisFrame ||
            Keyboard.current.slashKey.wasPressedThisFrame)
        {
            _player2Count++;
        }
    }

    // �X�}�z���́i���E����j

    private void HandleMobileInput()
    {
        if (Touchscreen.current == null) return;

        if (Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            Vector2 pos = Touchscreen.current.primaryTouch.position.ReadValue();

            if (pos.x < Screen.width / 2)
            {
                _player1Count++;
            }
            else
            {
                _player2Count++;
            }
        }
    }

    private void FinishGame(int winner)
    {
        _isRunning = false;
        float totalTime = Time.time - _startTime;

        Debug.Log($"Player {winner} wins! Time: {totalTime:F2}s | P1:{_player1Count}, P2:{_player2Count}");

        // TODO: ���U���g��ʂɑJ�ڂ��A���s�ƌ��ʂ�\��
        // ��: SceneManager.LoadScene("ResultScene");
    }
}
