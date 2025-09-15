using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ResultManager : MonoBehaviour
{
    [Header("�V�[����")]
    [SerializeField] private string _gameSceneName;
    [SerializeField] private string _titleSceneName;

    [Header("UI Image")]
    [SerializeField] private Image _gameImage;
    [SerializeField] private Image _titleImage;

    [Header("�z�[���h�b��")]
    [SerializeField] private float _requiredHoldTime = 2f;

    private float _holdTime1 = 0f;
    private float _holdTime2 = 0f;

    void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        // --- 1P: A or D�Ń^�C�g�� ---
        if (keyboard.aKey.isPressed || keyboard.dKey.isPressed)
        {
            _holdTime1 += Time.deltaTime;
            _titleImage.fillAmount = Mathf.Clamp01(_holdTime1 / _requiredHoldTime);

            if (_holdTime1 >= _requiredHoldTime)
                SceneManager.LoadScene(_titleSceneName);
        }
        else
        {
            _holdTime1 = 0f;
            _titleImage.fillAmount = 0f;
        }

        // --- 2P: J or L��InGame ---
        if (keyboard.jKey.isPressed || keyboard.lKey.isPressed)
        {
            _holdTime2 += Time.deltaTime;
            _gameImage.fillAmount = Mathf.Clamp01(_holdTime2 / _requiredHoldTime);

            if (_holdTime2 >= _requiredHoldTime)
                SceneManager.LoadScene(_gameSceneName);
        }
        else
        {
            _holdTime2 = 0f;
            _gameImage.fillAmount = 0f;
        }
    }
}
