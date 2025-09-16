using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class TutorialTextController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI messageText;

    [Header("Appear Animation")]
    [SerializeField] private float appearTime = 0.4f;   // �o���A�j���̎���

    [Header("Blink Animation")]
    [SerializeField] private float blinkMinAlpha = 0.4f; // �ŏ��A���t�@
    // [SerializeField] private float blinkMaxAlpha = 1f;   // �ő�A���t�@
    [SerializeField] private float blinkTime = 1f;       // �_�Ŏ���

    private Tween blinkTween;
    private Sequence showSequence;

    private void Awake()
    {
        if (messageText == null)
            messageText = GetComponent<TextMeshProUGUI>();

        ResetText();
    }

    /// <summary>
    /// �o���i�t�F�[�h�C�����_�ŊJ�n�j
    /// </summary>
    public void ShowMessage(string text)
    {
        ResetText();
        messageText.text = text;

        showSequence?.Kill();
        showSequence = DOTween.Sequence();

        // �t�F�[�h�C��
        showSequence.Append(messageText.DOFade(1f, appearTime));

        // �t�F�[�h�C��������ɓ_�ŊJ�n
        showSequence.OnComplete(() =>
        {
            blinkTween?.Kill();
            blinkTween = messageText.DOFade(blinkMinAlpha, blinkTime)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo); // �������[�v
        });
    }

    /// <summary>
    /// ������i�X�b�ƃt�F�[�h�A�E�g�j
    /// </summary>
    public void HideMessage()
    {
        blinkTween?.Kill();
        showSequence?.Kill();

        messageText.DOFade(0f, 1f).OnComplete(ResetText);
    }

    private void ResetText()
    {
        messageText.text = "";
        messageText.color = new Color(
            messageText.color.r,
            messageText.color.g,
            messageText.color.b,
            0f);
    }
}
