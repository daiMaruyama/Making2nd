using UnityEngine;
using DG.Tweening;

public class ShakeConstellation : MonoBehaviour
{
    [SerializeField] GameObject starPrefab;
    [SerializeField] Transform[] homePositions; // �����̐������ʒu�iEmpty����ׂĂ����j
    [SerializeField] int totalShakes = 100;

    private GameObject[] stars;
    private int shakeCount = 0;
    private int nextStarIndex = 0;

    void Start()
    {
        stars = new GameObject[homePositions.Length];

        // �����_���Ȉʒu�ɐ���z�u
        for (int i = 0; i < homePositions.Length; i++)
        {
            Vector2 randomPos = new Vector2(
                Random.Range(-5f, 5f),
                Random.Range(-8f, 8f)
            );

            stars[i] = Instantiate(starPrefab, randomPos, Quaternion.identity, transform);
        }
    }

    // �O������u�U�����v�ƌĂ�
    public void OnShake()
    {
        shakeCount++;

        int shakesPerStar = totalShakes / homePositions.Length;

        // ���񐔐U�邲�Ƃɐ��𐳂����ʒu�Ɉړ�
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
