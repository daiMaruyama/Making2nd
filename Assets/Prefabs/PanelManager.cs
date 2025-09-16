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

    // ==== ���ʁF�p�l���\�� + �z�o�[���Z�b�g ====
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

    // ==== �p�l���؂�ւ� ====
    public void ShowMain() => ShowPanel(mainPanel);
    public void ShowRanking() => ShowPanel(rankingPanel);
    public void ShowOnePlayer() => ShowPanel(onePlayerPanel);
    public void ShowTwoPlayer() => ShowPanel(twoPlayerPanel);
    public void ShowHowToPlay1() => ShowPanel(howToPlayPanel1);
    public void ShowHowToPlay2() => ShowPanel(howToPlayPanel2);

    // ==== �Q�[���J�� ====
    public void GoToGame(int mode)
    {
        // mode: 1 = 1�l�V���v��, 2 = 1�l�S, 3 = 2�l�ΐ�
        PlayerPrefs.SetInt("GameMode", mode);
        SceneManager.LoadScene(ingameSceneName);
    }

    // ==== �A�v���I���i�C�Ӂj ====
    public void QuitGame()
    {
        Application.Quit();
    }
}
