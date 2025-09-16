using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectMode : MonoBehaviour
{
    [Header("�p�l��")]
    [SerializeField] private GameObject panel_SelectMode;
    [SerializeField] private GameObject panel_SinglePlaySelect;
    [SerializeField] private GameObject panel_TwoPlayerSelect;

    [Header("�C���Q�[���V�[����")]
    [SerializeField] private string inGameSceneName = "InGame";

    // ===========================
    // ������ʃ{�^��
    // ===========================
    public void OnClickSinglePlay()
    {
        panel_SelectMode.SetActive(false);
        panel_SinglePlaySelect.SetActive(true);
    }

    public void OnClickTwoPlayer()
    {
        panel_SelectMode.SetActive(false);
        panel_TwoPlayerSelect.SetActive(true);
    }

    public void OnClickHowToPlay() { /* �V�ѕ��p�l���\�� */ }
    public void OnClickRanking() { /* �����L���O�p�l���\�� */ }

    // ===========================
    // 1�l�v���C��Փx�{�^��
    // ===========================
    public void OnSinglePlaySimple()
    {
        TapTwoPlayer.SetAIParameters(true, false);
        SceneManager.LoadScene(inGameSceneName);
    }

    public void OnSinglePlayOni()
    {
        TapTwoPlayer.SetAIParameters(true, true);
        SceneManager.LoadScene(inGameSceneName);
    }

    // ===========================
    // 2�l�v���C�X�^�[�g�{�^��
    // ===========================
    public void OnTwoPlayerStart()
    {
        TapTwoPlayer.SetAIParameters(false, false);
        SceneManager.LoadScene(inGameSceneName);
    }

    // ===========================
    // �߂�{�^��
    // ===========================
    public void OnClickBack()
    {
        panel_SelectMode.SetActive(true);
        panel_SinglePlaySelect.SetActive(false);
        panel_TwoPlayerSelect.SetActive(false);
    }
}
