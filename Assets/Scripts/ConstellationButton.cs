using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ConstellationButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("UI参照")]
    [SerializeField] private Image panelImage; // ボタン背景
    [SerializeField] private float hoverScale = 1.2f;
    [SerializeField] private Color hoverColor = Color.white;
    [SerializeField] private float tweenTime = 0.3f;

    private Vector3 originalScale;
    private Color originalColor;

    private void Awake()
    {
        if (panelImage == null) panelImage = GetComponent<Image>();
        originalScale = transform.localScale;
        originalColor = panelImage.color;

        // Idle演出（ゆらゆら）
        transform.DOScale(originalScale * 1.05f, 2f)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(originalScale * hoverScale, tweenTime).SetEase(Ease.OutBack);
        panelImage.DOColor(hoverColor, tweenTime);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(originalScale, tweenTime).SetEase(Ease.OutBack);
        panelImage.DOColor(originalColor, tweenTime);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // 決定演出：光ってからフェードアウト
        Sequence seq = DOTween.Sequence();
        seq.Append(panelImage.DOFade(1f, 0.1f));
        seq.Append(panelImage.DOColor(Color.yellow, 0.2f));
        seq.Append(transform.DOScale(originalScale * 1.4f, 0.3f).SetEase(Ease.OutBack));
        seq.Append(panelImage.DOFade(0f, 0.5f));
        seq.OnComplete(() =>
        {
            // ここでシーン遷移など呼ぶ
            Debug.Log("星座選択完了！");
        });
    }
}
