using UnityEngine;
using TMPro;
using System.Collections;

public class GameUIController : MonoBehaviour
{
    public static GameUIController Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI countdownText; // �����̉��o (3,2,1,Start / 5,4,3,2,1,TimeUp)
    [SerializeField] private TextMeshProUGUI timerText;     // �E��̏펞�c�莞��

    private void Awake()
    {
        Instance = this;
        if (countdownText != null) countdownText.text = "";
        if (timerText != null) timerText.text = "";
    }

    /// <summary>
    /// �Q�[���J�n�O�J�E���g�_�E��
    /// </summary>
    public IEnumerator StartCountdown(float waitBeforeStart = 1f)
    {
        yield return new WaitForSeconds(waitBeforeStart);

        int count = 3;
        while (count > 0)
        {
            countdownText.text = count.ToString();
            yield return new WaitForSeconds(1f);
            count--;
        }

        countdownText.text = "Start!";
        yield return new WaitForSeconds(1f);
        countdownText.text = "";

        TapTwoPlayer.Instance.StartGame();
    }

    /// <summary>
    /// �v���C���̎c�莞�Ԃ��E��ɕ\��
    /// </summary>
    public void UpdateTimer(float timeLeft)
    {
        timerText.text = $"Time: {Mathf.Ceil(timeLeft)}";
    }

    /// <summary>
    /// �^�񒆂ɁuTimeUp!�v��\��
    /// </summary>
    public void ShowTimeUp()
    {
        countdownText.text = "Time Up!";
    }

    public void ClearCountdown()
    {
        countdownText.text = "";
    }
}
