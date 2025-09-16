using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class HoverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("�z�o�[���̐F")]
    [SerializeField] private Color hoverColor = Color.gray;

    [Header("�A�j���[�V�����ݒ�")]
    [SerializeField] private float scaleUp = 1.1f;
    [SerializeField] private float duration = 0.2f;

    private Image buttonImage;
    private Color defaultColor;
    private Vector3 defaultScale;

    private Tween colorTween;
    private Tween scaleTween;

    private void Awake()
    {
        buttonImage = GetComponent<Image>();
        if (buttonImage != null)
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
        if (buttonImage == null || !gameObject.activeInHierarchy) return;

        // ������ Tween ������� Kill
        colorTween?.Kill();
        scaleTween?.Kill();

        if (isHover)
        {
            colorTween = buttonImage.DOColor(hoverColor, duration);
            scaleTween = transform.DOScale(defaultScale * scaleUp, duration).SetEase(Ease.OutBack);
        }
        else
        {
            colorTween = buttonImage.DOColor(defaultColor, duration);
            scaleTween = transform.DOScale(defaultScale, duration).SetEase(Ease.OutBack);
        }
    }

    public void ResetHover()
    {
        if (buttonImage != null)
            buttonImage.color = defaultColor;

        transform.localScale = defaultScale;

        if (EventSystem.current != null && EventSystem.current.currentSelectedGameObject == gameObject)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }

        // Tween ���m���� Kill
        colorTween?.Kill();
        scaleTween?.Kill();
    }

    private void OnDestroy()
    {
        // DOTween ���j���ς݃I�u�W�F�N�g�ɃA�N�Z�X���Ȃ��悤�ɂ���
        colorTween?.Kill();
        scaleTween?.Kill();
    }
}
