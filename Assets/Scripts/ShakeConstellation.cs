using UnityEngine;
using DG.Tweening;

public class ShakeConstellation : MonoBehaviour
{
    [SerializeField] GameObject starPrefab;
    [SerializeField] ConstellationData constellationData; // ScriptableObjectをアタッチ
    [SerializeField] int totalShakes = 100;

    private GameObject[] stars;
    private int shakeCount = 0;
    private int nextStarIndex = 0;

    void Start()
    {
        int starCount = stars.Length;
        stars = new GameObject[starCount];

        // ランダム位置に星を生成
        for (int i = 0; i < starCount; i++)
        {
            stars[i] = Instantiate(starPrefab, GetRandomStartPos(), Quaternion.identity, transform);
        }
    }

    public void OnShake()
    {
        shakeCount++;
        int shakesPerStar = totalShakes / stars.Length;

        if (shakeCount % shakesPerStar == 0 && nextStarIndex < stars.Length)
        {
            //MoveStarToHome(nextStarIndex);
            nextStarIndex++;
        }
    }

    Vector3 GetRandomStartPos()
    {
        Vector3 viewportPos = new Vector3(
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            Mathf.Abs(Camera.main.transform.position.z)
        );
        return Camera.main.ViewportToWorldPoint(viewportPos);
    }

    /*void MoveStarToHome(int index)
    {
        Vector2 normalized 
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(new Vector3(
            normalized.x,
            normalized.y,
            Mathf.Abs(Camera.main.transform.position.z)
        ));

        stars[index].transform.DOMove(worldPos, 1f).SetEase(Ease.OutQuad);
    }*/
}
