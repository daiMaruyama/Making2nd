using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class Constellation
{
    public string constellationName; // 星座名
    public int maxCount;             // その星座で生成される最大数
    public int starsToGenerate;      // 実際に生成される星の数
}

public class ShakePhaseController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CameraController cameraController;  // カメラ揺れ用
    [SerializeField] private GameObject starPrefab;               // 星パーツPrefab
    [SerializeField] private Transform spawnArea;                 // 星生成範囲

    [Header("Settings")]
    [SerializeField] private Constellation constellation;         // 星座設定

    private bool hasGeneratedStars = false;  // 生成済みかどうか

    void Update()
    {
        bool shakeDetected = false;

        // マウスクリック
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            shakeDetected = true;

        // スマホタッチ
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
            shakeDetected = true;

        if (shakeDetected)
        {
            // クリック／タップでカメラ揺れ
            if (cameraController != null)
                cameraController.Shake();

            // 星生成（まだ生成していなければ）
            if (!hasGeneratedStars)
            {
                GenerateStars();
                hasGeneratedStars = true;
            }
        }
    }

    private void GenerateStars()
    {
        if (constellation == null || starPrefab == null || spawnArea == null)
            return;

        int count = Mathf.Clamp(constellation.starsToGenerate, 0, constellation.maxCount);

        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPos = new Vector3(
                Random.Range(-spawnArea.localScale.x / 2f, spawnArea.localScale.x / 2f),
                Random.Range(-spawnArea.localScale.y / 2f, spawnArea.localScale.y / 2f),
                0f
            );

            Instantiate(starPrefab, spawnArea.position + spawnPos, Quaternion.identity);
        }

        Debug.Log($"星座「{constellation.constellationName}」の星を{count}個生成");
    }
}
