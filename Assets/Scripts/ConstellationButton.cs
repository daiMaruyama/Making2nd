using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ConstellationButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("UI�Q��")]
    [SerializeField] private Image panelImage; // �{�^���w�i
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

        // Idle���o�i�����j
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
        // ���艉�o�F�����Ă���t�F�[�h�A�E�g
        Sequence seq = DOTween.Sequence();
        seq.Append(panelImage.DOFade(1f, 0.1f));
        seq.Append(panelImage.DOColor(Color.yellow, 0.2f));
        seq.Append(transform.DOScale(originalScale * 1.4f, 0.3f).SetEase(Ease.OutBack));
        seq.Append(panelImage.DOFade(0f, 0.5f));
        seq.OnComplete(() =>
        {
            // �����ŃV�[���J�ڂȂǌĂ�
            Debug.Log("�����I�������I");
        });
    }
}
