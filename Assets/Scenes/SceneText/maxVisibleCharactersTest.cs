/// <summary>
/// ���̃^�C�v���C�^�[�����́A�����񂪍ŏ����� TMP_Text �ɐݒ肳��Ă���ꍇ�ɗL���B
/// - �����̈ʒu���Œ肳��� (������͍ŏ����瑶��)
/// - TMP �̎������C�A�E�g�����̂܂܎g���� (TMP �������Ɍv�Z�������C�A�E�g�����̂܂܎g��)
/// /// </summary>

using System.Collections;
using TMPro;
using UnityEngine;

public class MaxVisibleCharactersTest : MonoBehaviour
{
    [SerializeField] TMP_Text _text;
    [SerializeField] float _delay = 0.1f; // ��������̊Ԋu

    void Start()
    {
        _text.maxVisibleCharacters = 0;
        if (_text == null)
            Debug.Log("�Ώۃe�L�X�g���A�^�b�`���Ă��������B");

        if (_text != null)
            StartCoroutine(FlowText());
    }

    IEnumerator FlowText()
    {
        int totalChars = _text.text.Length;

        for (int i = 0; i <= totalChars; i++)
        {
            _text.maxVisibleCharacters = i;
            yield return new WaitForSeconds(_delay);
        }
    }
}
