using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class ShakePhaseController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CameraController cameraController;
    [SerializeField] private GameObject starPrefab;

    private ConstellationData constellation;
    private int totalShakes;

    private GameObject[] stars;
    private bool hasGeneratedStars = false;
    private int shakeCount = 0;
    private int nextStarIndex = 0;

    void Start()
    {
        // GameManager から取得
        if (GameManager.Instance != null)
        {
            constellation = GameManager.Instance.selectedConstellation;
            totalShakes = GameManager.Instance.totalShakes;
        }

        // 星をランダム生成
        if (constellation != null && starPrefab != null)
        {
            int count = Mathf.Clamp(constellation.starsToGenerate, 0, constellation.maxCount);
            stars = new GameObject[count];

            for (int i = 0; i < count; i++)
            {
                Vector3 viewportPos = new Vector3(
                    Random.Range(0f, 1f),
                    Random.Range(0f, 1f),
                    Mathf.Abs(Camera.main.transform.position.z)
                );

                Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewportPos);
                stars[i] = Instantiate(starPrefab, worldPos, Quaternion.identity);
            }
        }
    }

    void Update()
    {
        bool shakeDetected = false;

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            shakeDetected = true;
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
            shakeDetected = true;
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
            shakeDetected = true; // PCデバッグ用

        if (shakeDetected)
        {
            cameraController?.Shake();

            if (!hasGeneratedStars)
            {
                hasGeneratedStars = true; // 星は Start() で生成済みの場合は無視
            }

            HandleShake();
        }
    }

    private void HandleShake()
    {
        if (stars == null || stars.Length == 0)
            return;

        shakeCount++;
        int shakesPerStar = totalShakes / stars.Length;

        if (shakeCount % shakesPerStar == 0 && nextStarIndex < stars.Length)
        {
            MoveStarToHome(nextStarIndex);
            nextStarIndex++;
        }
    }

    private void MoveStarToHome(int index)
    {
        Vector2 normalized = constellation.starsNormalized[index];
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(new Vector3(
            normalized.x,
            normalized.y,
            Mathf.Abs(Camera.main.transform.position.z)
        ));

        stars[index].transform.DOMove(worldPos, 1f).SetEase(Ease.OutQuad);
    }
}
