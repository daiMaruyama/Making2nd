using UnityEngine;
using DG.Tweening;

public class ShakeConstellation : MonoBehaviour
{
    [SerializeField] GameObject starPrefab;
    [SerializeField] Transform[] homePositions; // 星座の正しい位置（Emptyを並べておく）
    [SerializeField] int totalShakes = 100;

    private GameObject[] stars;
    private int shakeCount = 0;
    private int nextStarIndex = 0;

    void Start()
    {
        stars = new GameObject[homePositions.Length];

        // ランダムな位置に星を配置
        for (int i = 0; i < homePositions.Length; i++)
        {
            Vector2 randomPos = new Vector2(
                Random.Range(-5f, 5f),
                Random.Range(-8f, 8f)
            );

            stars[i] = Instantiate(starPrefab, randomPos, Quaternion.identity, transform);
        }
    }

    // 外部から「振った」と呼ぶ
    public void OnShake()
    {
        shakeCount++;

        int shakesPerStar = totalShakes / homePositions.Length;

        // 一定回数振るごとに星を正しい位置に移動
        if (shakeCount % shakesPerStar == 0 && nextStarIndex < stars.Length)
        {
            MoveStarToHome(nextStarIndex);
            nextStarIndex++;
        }
    }

    void MoveStarToHome(int index)
    {
        stars[index].transform.DOMove(
            homePositions[index].position,
            1f
        ).SetEase(Ease.OutQuad);
    }
}
