using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject rankingPanel;
    [SerializeField] private GameObject onePlayerPanel;
    [SerializeField] private GameObject twoPlayerPanel;
    [SerializeField] private GameObject howToPlayPanel1;
    [SerializeField] private GameObject howToPlayPanel2;

    [Header("Game Scene Name")]
    [SerializeField] private string ingameSceneName = "Ingame";

    private void Start()
    {
        ShowMain();
    }

    private void HideAll()
    {
        mainPanel.SetActive(false);
        rankingPanel.SetActive(false);
        onePlayerPanel.SetActive(false);
        twoPlayerPanel.SetActive(false);
        howToPlayPanel1.SetActive(false);
        howToPlayPanel2.SetActive(false);
    }

    // ==== 共通：パネル表示 + ホバーリセット ====
    private void ShowPanel(GameObject panel)
    {
        HideAll();
        panel.SetActive(true);
        ResetAllHoverButtons(panel);
    }

    private void ResetAllHoverButtons(GameObject panel)
    {
        HoverButton[] buttons = panel.GetComponentsInChildren<HoverButton>();
        foreach (var btn in buttons)
        {
            btn.ResetHover();
        }
    }

    // ==== パネル切り替え ====
    public void ShowMain() => ShowPanel(mainPanel);
    public void ShowRanking() => ShowPanel(rankingPanel);
    public void ShowOnePlayer() => ShowPanel(onePlayerPanel);
    public void ShowTwoPlayer() => ShowPanel(twoPlayerPanel);
    public void ShowHowToPlay1() => ShowPanel(howToPlayPanel1);
    public void ShowHowToPlay2() => ShowPanel(howToPlayPanel2);

    // ==== ゲーム遷移 ====
    public void GoToGame(int mode)
    {
        PlayerPrefs.SetInt("GameMode", mode);
        StartCoroutine(LoadGameAfterDelay(1f)); // 1秒待って遷移
    }

    private IEnumerator LoadGameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(ingameSceneName);
    }

    // ==== アプリ終了（任意） ====
    public void QuitGame()
    {
        Application.Quit();
    }
}
