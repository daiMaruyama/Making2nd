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

    // �p�l���؂�ւ����ɌĂԃ��Z�b�g�p�֐�
    public void ResetHover()
    {
        // DOTween�͏u���ɖ߂�
        buttonImage.color = defaultColor;
        transform.localScale = defaultScale;

        // EventSystem��̑I��������
        if (EventSystem.current != null && EventSystem.current.currentSelectedGameObject == gameObject)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
}
