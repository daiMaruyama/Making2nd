using UnityEngine;
using TMPro;
using DG.Tweening;

public class FinishUIController : MonoBehaviour
{
    public static FinishUIController Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI finishText;

    private void Awake()
    {
        Instance = this;
        finishText.alpha = 0;
    }

    public void ShowFinish(int winner)
    {
        finishText.text = $"FINISH!\nPlayer {winner} Wins!";
        finishText.alpha = 0;

        finishText.DOFade(1f, 1f).SetEase(Ease.OutQuad);
        finishText.transform.DOScale(1.2f, 0.6f).SetLoops(2, LoopType.Yoyo);
    }
}
