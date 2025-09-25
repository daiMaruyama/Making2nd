/// <summary>
/// TMP_Text に最初から設定されている文字列をタイプライター風に表示する処理。
/// - foreach で文字を一文字ずつ追加し、await Task.Delay で文字送り間隔を制御
/// - maxVisibleCharacters は使用せず、_text.text に += で文字を追加
/// </summary>

using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class ForeachTest : MonoBehaviour
{
    [SerializeField] TMP_Text _text;
    [SerializeField] float _delay = 0.1f; // 文字送り間隔
    string _textBox;

    async void Start()
    {
        if (_text == null) _text = GetComponent<TMP_Text>();
        _textBox = _text.text;
        _text.text = "";  // 最初は空に
        await TypeTextAsync();
    }

    async Task TypeTextAsync()
    {
        foreach (char c in _textBox)
        {
            await Task.Delay((int)(_delay * 1000)); // ミリ秒
            _text.text += c;
        }
    }
}
