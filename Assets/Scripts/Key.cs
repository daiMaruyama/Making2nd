using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

public class Key : MonoBehaviour
{
    [Header("�ΏۂƂȂ�Image 4�� (A, D, J, L �ɑΉ�)")]
    [SerializeField] private Image[] _keyImages;

    [Header("�Â����銄�� (1 = ���̂܂�, 0.5 = �����̖��邳)")]
    [SerializeField] private float _darkenFactor = 0.6f;

    private Color[] _originalColors;

    private void Awake()
    {
        // ���̐F��ۑ�
        _originalColors = new Color[_keyImages.Length];
        for (int i = 0; i < _keyImages.Length; i++)
        {
            if (_keyImages[i] != null)
                _originalColors[i] = _keyImages[i].color;
        }
    }

    private void Update()
    {
        // A
        HandleKey(Keyboard.current.aKey, 0);
        // D
        HandleKey(Keyboard.current.dKey, 1);
        // J
        HandleKey(Keyboard.current.jKey, 2);
        // L
        HandleKey(Keyboard.current.lKey, 3);
    }

    private void HandleKey(KeyControl key, int index)
    {
        if (_keyImages[index] == null) return;

        if (key.isPressed)
        {
            Color c = _originalColors[index] * _darkenFactor;
            c.a = _originalColors[index].a; // ���͌��̂܂�
            _keyImages[index].color = c;
        }
        else
        {
            _keyImages[index].color = _originalColors[index];
        }
    }
}
