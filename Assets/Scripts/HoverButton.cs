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
        SetHover(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SetHover(false);
    }

    private void SetHover(bool isHover)
    {
        if (isHover)
        {
            buttonImage.DOColor(hoverColor, duration);
            transform.DOScale(defaultScale * scaleUp, duration).SetEase(Ease.OutBack);
        }
        else
        {
            buttonImage.DOColor(defaultColor, duration);
            transform.DOScale(defaultScale, duration).SetEase(Ease.OutBack);
        }
    }

    // パネル切り替え時に呼ぶリセット用関数
    public void ResetHover()
    {
        // DOTweenは瞬時に戻す
        buttonImage.color = defaultColor;
        transform.localScale = defaultScale;

        // EventSystem上の選択も解除
        if (EventSystem.current != null && EventSystem.current.currentSelectedGameObject == gameObject)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
}
