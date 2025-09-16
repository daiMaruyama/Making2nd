using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class TutorialTextController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI messageText;

    [Header("Appear Animation")]
    [SerializeField] private float appearTime = 0.4f;   // 出現アニメの時間

    [Header("Blink Animation")]
    [SerializeField] private float blinkMinAlpha = 0.4f; // 最小アルファ
    // [SerializeField] private float blinkMaxAlpha = 1f;   // 最大アルファ
    [SerializeField] private float blinkTime = 1f;       // 点滅周期

    private Tween blinkTween;
    private Sequence showSequence;

    private void Awake()
    {
        if (messageText == null)
            messageText = GetComponent<TextMeshProUGUI>();

        ResetText();
    }

    /// <summary>
    /// 出現（フェードイン→点滅開始）
    /// </summary>
    public void ShowMessage(string text)
    {
        ResetText();
        messageText.text = text;

        showSequence?.Kill();
        showSequence = DOTween.Sequence();

        // フェードイン
        showSequence.Append(messageText.DOFade(1f, appearTime));

        // フェードイン完了後に点滅開始
        showSequence.OnComplete(() =>
        {
            blinkTween?.Kill();
            blinkTween = messageText.DOFade(blinkMinAlpha, blinkTime)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo); // 往復ループ
        });
    }

    /// <summary>
    /// 消える（スッとフェードアウト）
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
