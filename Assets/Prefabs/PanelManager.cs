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

    // ==== パネル切り替え ====
    public void ShowMain()
    {
        HideAll();
        mainPanel.SetActive(true);
    }

    public void ShowRanking()
    {
        HideAll();
        rankingPanel.SetActive(true);
    }

    public void ShowOnePlayer()
    {
        HideAll();
        onePlayerPanel.SetActive(true);
    }

    public void ShowTwoPlayer()
    {
        HideAll();
        twoPlayerPanel.SetActive(true);
    }

    public void ShowHowToPlay1()
    {
        HideAll();
        howToPlayPanel1.SetActive(true);
    }

    public void ShowHowToPlay2()
    {
        HideAll();
        howToPlayPanel2.SetActive(true);
    }

    // ==== ゲーム遷移 ====
    public void GoToGame(int mode)
    {
        // mode: 1 = 1人シンプル, 2 = 1人鬼, 3 = 2人対戦 とかフラグに使える
        PlayerPrefs.SetInt("GameMode", mode);
        SceneManager.LoadScene(ingameSceneName);
    }

    // ==== アプリ終了（任意） ====
    public void QuitGame()
    {
        Application.Quit();
    }
}
