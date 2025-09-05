using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class SelectionUI : MonoBehaviour
{
    [SerializeField] private ConstellationData constellation; // ���̃{�^���ɑΉ����鐯��
    [SerializeField] private Image panel; // �{�^���̌����ځiImage�j

    [SerializeField] private string sceneName = "InGame"; // �J�ڐ�͋���

    public void OnSelect()
    {
        if (constellation == null)
        {
            Debug.LogWarning("ConstellationData is not assigned!");
            return;
        }

        // GameManager �ɑI����n��
        GameManager.Instance.SelectConstellation(constellation);

        // ���o �� �V�[���J��
        Sequence seq = DOTween.Sequence();
        seq.Append(panel.transform.DOScale(1.2f, 0.3f).SetEase(Ease.OutBack));
        seq.Join(panel.DOFade(0f, 0.3f));
        seq.OnComplete(() =>
        {
            SceneManager.LoadScene(sceneName);
        });
    }
}
