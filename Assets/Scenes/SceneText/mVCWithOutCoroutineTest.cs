/// <summary>
/// �R���[�`����p������Update���ŕ�������𐧌䂷��^�C�v���C�^�[���ʁB
/// - �����̈ʒu�͌Œ肳��ATMP �̎������C�A�E�g�����̂܂܎g����
/// - ��������̑��x�� _delay �Œ����\
/// </summary>
/// 
using TMPro;
using UnityEngine;

public class mVCWithOutCoroutineTest : MonoBehaviour
{
    [SerializeField] TMP_Text _text;
    [SerializeField] float _delay = 0.1f; // ��������̊Ԋu [�b]

    bool _isRunning = true;
    float _timer;
    int _currentMVC = 0;

    void Start()
    {
        if (_text == null)
        {
            Debug.LogError("TMP_Text ���A�^�b�`����Ă��܂���B");
            enabled = false; // �X�N���v�g���~���ăG���[���
            return;
        }

        _text.maxVisibleCharacters = 0; // �ŏ��͕�����\��
        _timer = _delay;
    }

    void Update()
    {
        if (!_isRunning) return;

        // ���̕����\���܂ł̎��Ԃ����炷
        _timer -= Time.deltaTime;
        if (_timer > 0) return;

        // ������1�\��
        _currentMVC++;
        _text.maxVisibleCharacters = _currentMVC;

        // �^�C�}�[���Z�b�g
        _timer = _delay;

        // �S�Ă̕�����\���������~
        if (_currentMVC >= _text.text.Length)
            _isRunning = false;
    }
}
