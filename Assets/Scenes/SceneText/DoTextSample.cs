using TMPro;
using UnityEngine;
using DG.Tweening;

public class DoTextSample : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _testTMP;
    string _text;
    void Start()
    {
        _testTMP = GetComponent<TextMeshProUGUI>();
        _text = _testTMP.text;
    }

    void ShowDoText()
    {
        if (_testTMP != null)
        {
            _testTMP.text = _text;
            _testTMP.DOText(_text, 5f);
        }
    }
}
