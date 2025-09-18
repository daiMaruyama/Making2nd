using UnityEngine;

public class SkyboxAutoShaker : MonoBehaviour
{
    [SerializeField] private float shakeAmplitude = 15f; // �����_���ɓ����ő�p�x�i�x�j
    [SerializeField] private float changeInterval = 1f;  // �������ς��Ԋu�i�b�j
    [SerializeField] private float lerpSpeed = 2f;       // ��ԃX�s�[�h�i�l��傫������ƃL�r�L�r�����j

    private float _baseRotation;
    private float _targetRotation;
    private float _currentRotation;
    private float _timer;

    private void Start()
    {
        if (RenderSettings.skybox != null)
        {
            _baseRotation = RenderSettings.skybox.GetFloat("_Rotation");
            _currentRotation = _baseRotation;
            _targetRotation = _baseRotation;
        }
    }

    private void Update()
    {
        if (RenderSettings.skybox == null) return;

        _timer += Time.deltaTime;

        // ���Ԋu���ƂɐV���������_���p�x������
        if (_timer >= changeInterval)
        {
            _targetRotation = _baseRotation + Random.Range(-shakeAmplitude, shakeAmplitude);
            _timer = 0f;
        }

        // ���݂̊p�x���^�[�Q�b�g�ɕ��
        _currentRotation = Mathf.Lerp(_currentRotation, _targetRotation, Time.deltaTime * lerpSpeed);

        // Skybox �ɔ��f
        RenderSettings.skybox.SetFloat("_Rotation", _currentRotation);
    }
}
