using UnityEngine;
using UnityEngine.InputSystem; // 新インプットシステム

public class TapCount : MonoBehaviour
{
    [SerializeField] int _limitTapCount = 100; // 規定回数
     int _currentTapCount = 0;

    float _startTime;
    float _endTime;
    bool _isRunning = false;

    public delegate void GameFinished(float time, int totalTaps);
    public event GameFinished OnGameFinished;

    private void Start()
    {
        _currentTapCount = 0;
        _isRunning = true;
        _startTime = Time.time;
    }

    private void Update()
    {
        if (!_isRunning) return;

        // PC入力: スペースキー
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            AddTap();
            Debug.Log(_currentTapCount);
        }

        // スマホ入力: 画面タップ
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            AddTap();
        }
    }

    private void AddTap()
    {
        _currentTapCount++;

        if (_currentTapCount >= _limitTapCount)
        {
            FinishGame();
        }
    }

    private void FinishGame()
    {
        _isRunning = false;
        _endTime = Time.time;
        float totalTime = _endTime - _startTime;

        Debug.Log($"Game Finished! Time: {totalTime:F2} sec, Taps: {_currentTapCount}");

        // リザルト画面に渡す用のイベント呼び出し
        OnGameFinished?.Invoke(totalTime, _currentTapCount);
    }
}
