using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectMode : MonoBehaviour
{
    [Header("インゲームシーン名")]
    [SerializeField] private string inGameSceneName = "InGame";

    // ===========================
    // 1人プレイ（シンプルモード）
    // ===========================
    public void OnSinglePlaySimple()
    {
        LoadInGameScene(true, false); // AI有効、シンプルモード
    }

    // ===========================
    // 1人プレイ（鬼モード）
    // ===========================
    public void OnSinglePlayOni()
    {
        LoadInGameScene(true, true); // AI有効、鬼モード（ゴースト相当）
    }

    // ===========================
    // 2人プレイ
    // ===========================
    public void OnTwoPlayer()
    {
        LoadInGameScene(false, false); // AI無効
    }

    // ===========================
    // 共通シーン遷移処理
    // ===========================
    private void LoadInGameScene(bool isAI, bool isOniMode)
    {
        // インゲームに渡すAI設定
        TapTwoPlayer.SetAIParameters(isAI, isOniMode);

        // シーン遷移
        SceneManager.LoadScene(inGameSceneName);
    }
}
