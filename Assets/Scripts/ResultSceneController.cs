using UnityEngine;

public class ResultSceneController : MonoBehaviour
{
    [Header("勝者表示オブジェクト")]
    [SerializeField] private GameObject p1WinObject;
    [SerializeField] private GameObject p2WinObject;
    [SerializeField] private GameObject drawObject;

    private void Start()
    {
        ShowWinner();
    }

    private void ShowWinner()
    {
        if (GameResultManager.Instance == null)
        {
            Debug.LogError("GameResultManagerが存在しません！");
            return;
        }

        var (winner, _, _, _) = GameResultManager.Instance.GetResult();

        // まず全部非表示
        p1WinObject?.SetActive(false);
        p2WinObject?.SetActive(false);
        drawObject?.SetActive(false);

        // 勝者表示
        switch (winner)
        {
            case 1:
                p1WinObject?.SetActive(true);
                break;
            case 2:
                p2WinObject?.SetActive(true);
                break;
            case 0:
                drawObject?.SetActive(true);
                break;
        }
    }
}
