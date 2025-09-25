/// <summary>
/// TMP_Text �ɍŏ�����ݒ肳��Ă��镶������^�C�v���C�^�[���ɕ\�����鏈���B
/// - foreach �ŕ������ꕶ�����ǉ����Aawait Task.Delay �ŕ�������Ԋu�𐧌�
/// - maxVisibleCharacters �͎g�p�����A_text.text �� += �ŕ�����ǉ�
/// </summary>

using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class ForeachTest : MonoBehaviour
{
    [SerializeField] TMP_Text _text;
    [SerializeField] float _delay = 0.1f; // ��������Ԋu
    string _textBox;

    async void Start()
    {
        if (_text == null) _text = GetComponent<TMP_Text>();
        _textBox = _text.text;
        _text.text = "";  // �ŏ��͋��
        await TypeTextAsync();
    }

    async Task TypeTextAsync()
    {
        foreach (char c in _textBox)
        {
            await Task.Delay((int)(_delay * 1000)); // �~���b
            _text.text += c;
        }
    }
}
