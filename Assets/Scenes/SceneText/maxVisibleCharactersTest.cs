/// <summary>
/// このタイプライター処理は、文字列が最初から TMP_Text に設定されている場合に有効。
/// - 文字の位置が固定される (文字列は最初から存在)
/// - TMP の自動レイアウトがそのまま使える (TMP が初期に計算したレイアウトをそのまま使う)
/// /// </summary>

using System.Collections;
using TMPro;
using UnityEngine;

public class MaxVisibleCharactersTest : MonoBehaviour
{
    [SerializeField] TMP_Text _text;
    [SerializeField] float _delay = 0.1f; // 文字送りの間隔

    void Start()
    {
        if (_text == null)
            _text = GetComponent<TMP_Text>();

        if (_text != null)
            StartCoroutine(FlowText());
    }

    private IEnumerator FlowText()
    {
        _text.maxVisibleCharacters = 0;
        int totalChars = _text.text.Length;

        for (int i = 1; i <= totalChars; i++)
        {
            _text.maxVisibleCharacters = i;
            yield return new WaitForSeconds(_delay);
        }
    }
}
