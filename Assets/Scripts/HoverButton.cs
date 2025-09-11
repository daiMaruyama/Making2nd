using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class HoverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("ホバー時の色")]
    [SerializeField] private Color hoverColor = Color.gray;

    [Header("アニメーション設定")]
    [SerializeField] private float scaleUp = 1.1f;
    [SerializeField] private float duration = 0.2f;

    private Image buttonImage;
    private Color defaultColor;
    private Vector3 defaultScale;

    private void Awake()
    {
        buttonImage = GetComponent<Image>();
        defaultColor = buttonImage.color;
        defaultScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // 色を濃くする
        buttonImage.DOColor(hoverColor, duration);

        // 少し大きくする
        transform.DOScale(defaultScale * scaleUp, duration).SetEase(Ease.OutBack);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // 元の色に戻す
        buttonImage.DOColor(defaultColor, duration);

        // 元のサイズに戻す
        transform.DOScale(defaultScale, duration).SetEase(Ease.OutBack);
    }
}
