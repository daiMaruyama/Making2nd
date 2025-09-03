using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class Constellation
{
    public string constellationName; // ������
    public int maxCount;             // ���̐����Ő��������ő吔
    public int starsToGenerate;      // ���ۂɐ�������鐯�̐�
}

public class ShakePhaseController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CameraController cameraController;  // �J�����h��p
    [SerializeField] private GameObject starPrefab;               // ���p�[�cPrefab
    [SerializeField] private Transform spawnArea;                 // �������͈�

    [Header("Settings")]
    [SerializeField] private Constellation constellation;         // �����ݒ�

    private bool hasGeneratedStars = false;  // �����ς݂��ǂ���

    void Update()
    {
        bool shakeDetected = false;

        // �}�E�X�N���b�N
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            shakeDetected = true;

        // �X�}�z�^�b�`
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
            shakeDetected = true;

        if (shakeDetected)
        {
            // �N���b�N�^�^�b�v�ŃJ�����h��
            if (cameraController != null)
                cameraController.Shake();

            // �������i�܂��������Ă��Ȃ���΁j
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

        Debug.Log($"�����u{constellation.constellationName}�v�̐���{count}����");
    }
}
