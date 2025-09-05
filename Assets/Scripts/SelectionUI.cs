using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class SelectionUI : MonoBehaviour
{
    [SerializeField] private ConstellationData constellation; // このボタンに対応する星座
    [SerializeField] private Image panel; // ボタンの見た目（Image）

    [SerializeField] private string sceneName = "InGame"; // 遷移先は共通

    public void OnSelect()
    {
        if (constellation == null)
        {
            Debug.LogWarning("ConstellationData is not assigned!");
            return;
        }

        // GameManager に選択を渡す
        GameManager.Instance.SelectConstellation(constellation);

        // 演出 → シーン遷移
        Sequence seq = DOTween.Sequence();
        seq.Append(panel.transform.DOScale(1.2f, 0.3f).SetEase(Ease.OutBack));
        seq.Join(panel.DOFade(0f, 0.3f));
        seq.OnComplete(() =>
        {
            SceneManager.LoadScene(sceneName);
        });
    }
}
