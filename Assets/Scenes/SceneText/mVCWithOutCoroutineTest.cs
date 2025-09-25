/// <summary>
/// コルーチンを用いずにUpdate内で文字送りを制御するタイプライター効果。
/// - 文字の位置は固定され、TMP の自動レイアウトがそのまま使える
/// - 文字送りの速度は _delay で調整可能
/// </summary>
/// 
using TMPro;
using UnityEngine;

public class mVCWithOutCoroutineTest : MonoBehaviour
{
    [SerializeField] TMP_Text _text;
    [SerializeField] float _delay = 0.1f; // 文字送りの間隔 [秒]

    bool _isRunning = true;
    float _timer;
    int _currentMVC = 0;

    void Start()
    {
        if (_text == null)
        {
            Debug.LogError("TMP_Text がアタッチされていません。");
            enabled = false; // スクリプトを停止してエラー回避
            return;
        }

        _text.maxVisibleCharacters = 0; // 最初は文字非表示
        _timer = _delay;
    }

    void Update()
    {
        if (!_isRunning) return;

        // 次の文字表示までの時間を減らす
        _timer -= Time.deltaTime;
        if (_timer > 0) return;

        // 文字を1つ表示
        _currentMVC++;
        _text.maxVisibleCharacters = _currentMVC;

        // タイマーリセット
        _timer = _delay;

        // 全ての文字を表示したら停止
        if (_currentMVC >= _text.text.Length)
            _isRunning = false;
    }
}
