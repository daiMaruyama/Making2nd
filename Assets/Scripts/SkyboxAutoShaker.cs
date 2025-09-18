using UnityEngine;

public class SkyboxAutoShaker : MonoBehaviour
{
    [SerializeField] private float shakeAmplitude = 15f; // ランダムに動く最大角度（度）
    [SerializeField] private float changeInterval = 1f;  // 向きが変わる間隔（秒）
    [SerializeField] private float lerpSpeed = 2f;       // 補間スピード（値を大きくするとキビキビ動く）

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

        // 一定間隔ごとに新しいランダム角度を決定
        if (_timer >= changeInterval)
        {
            _targetRotation = _baseRotation + Random.Range(-shakeAmplitude, shakeAmplitude);
            _timer = 0f;
        }

        // 現在の角度をターゲットに補間
        _currentRotation = Mathf.Lerp(_currentRotation, _targetRotation, Time.deltaTime * lerpSpeed);

        // Skybox に反映
        RenderSettings.skybox.SetFloat("_Rotation", _currentRotation);
    }
}
