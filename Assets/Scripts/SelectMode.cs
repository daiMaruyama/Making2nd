using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectMode : MonoBehaviour
{
    [Header("パネル")]
    [SerializeField] private GameObject panel_SelectMode;
    [SerializeField] private GameObject panel_SinglePlaySelect;
    [SerializeField] private GameObject panel_TwoPlayerSelect;

    [Header("インゲームシーン名")]
    [SerializeField] private string inGameSceneName = "InGame";

    // ===========================
    // 初期画面ボタン
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

    public void OnClickHowToPlay() { /* 遊び方パネル表示 */ }
    public void OnClickRanking() { /* ランキングパネル表示 */ }

    // ===========================
    // 1人プレイ難易度ボタン
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
    // 2人プレイスタートボタン
    // ===========================
    public void OnTwoPlayerStart()
    {
        TapTwoPlayer.SetAIParameters(false, false);
        SceneManager.LoadScene(inGameSceneName);
    }

    // ===========================
    // 戻るボタン
    // ===========================
    public void OnClickBack()
    {
        panel_SelectMode.SetActive(true);
        panel_SinglePlaySelect.SetActive(false);
        panel_TwoPlayerSelect.SetActive(false);
    }
}
