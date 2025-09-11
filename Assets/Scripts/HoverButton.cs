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
        // �F��Z������
        buttonImage.DOColor(hoverColor, duration);

        // �����傫������
        transform.DOScale(defaultScale * scaleUp, duration).SetEase(Ease.OutBack);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // ���̐F�ɖ߂�
        buttonImage.DOColor(defaultColor, duration);

        // ���̃T�C�Y�ɖ߂�
        transform.DOScale(defaultScale, duration).SetEase(Ease.OutBack);
    }
}
