using UnityEngine;
using UnityEngine.SceneManagement;

public class GameResultManager : MonoBehaviour
{
    public static GameResultManager Instance { get; private set; }

    private int _winner;
    private int _p1Count;
    private int _p2Count;

    [SerializeField] private float delayBeforeSceneChange = 3f; // �V���A���C�Y�Őݒ�
    [SerializeField] string sceneName;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetResult(int winner, int p1, int p2)
    {
        _winner = winner;
        _p1Count = p1;
        _p2Count = p2;

        // �����L���O�ۑ��i���҂̃X�R�A��ۑ��j
        int scoreToSave = (winner == 1) ? _p1Count : (winner == 2 ? _p2Count : Mathf.Max(_p1Count, _p2Count));
        SaveRecord(scoreToSave);

        // Finish���o�\��
        FinishUIController.Instance?.ShowFinish(winner);

        // ���b��ɑJ��
        Invoke(nameof(GoToResultScene), delayBeforeSceneChange);
    }

    private void SaveRecord(int newScore)
    {
        const int MaxRank = 5;

        // �����̃����L���O���擾
        int[] scores = new int[MaxRank];
        for (int i = 0; i < MaxRank; i++)
        {
            scores[i] = PlayerPrefs.GetInt($"BestScore{i}", 0);
        }

        // �V�L�^��}���i�~���F�X�R�A�傫���قǏ�j
        for (int i = 0; i < MaxRank; i++)
        {
            if (newScore > scores[i])
            {
                for (int j = MaxRank - 1; j > i; j--)
                {
                    scores[j] = scores[j - 1];
                }
                scores[i] = newScore;
                break;
            }
        }

        // �ۑ�
        for (int i = 0; i < MaxRank; i++)
        {
            PlayerPrefs.SetInt($"BestScore{i}", scores[i]);
        }

        PlayerPrefs.Save();
    }

    private void GoToResultScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    // ResultScene �ŌĂԗp
    public (int winner, int p1, int p2) GetResult()
    {
        return (_winner, _p1Count, _p2Count);
    }
}
