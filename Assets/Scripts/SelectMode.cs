using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectMode : MonoBehaviour
{
    [Header("�C���Q�[���V�[����")]
    [SerializeField] private string inGameSceneName = "InGame";

    // ===========================
    // 1�l�v���C�i�V���v�����[�h�j
    // ===========================
    public void OnSinglePlaySimple()
    {
        LoadInGameScene(true, false); // AI�L���A�V���v�����[�h
    }

    // ===========================
    // 1�l�v���C�i�S���[�h�j
    // ===========================
    public void OnSinglePlayOni()
    {
        LoadInGameScene(true, true); // AI�L���A�S���[�h�i�S�[�X�g�����j
    }

    // ===========================
    // 2�l�v���C
    // ===========================
    public void OnTwoPlayer()
    {
        LoadInGameScene(false, false); // AI����
    }

    // ===========================
    // ���ʃV�[���J�ڏ���
    // ===========================
    private void LoadInGameScene(bool isAI, bool isOniMode)
    {
        // �C���Q�[���ɓn��AI�ݒ�
        TapTwoPlayer.SetAIParameters(isAI, isOniMode);

        // �V�[���J��
        SceneManager.LoadScene(inGameSceneName);
    }
}
