using UnityEngine;
using UnityEngine.SceneManagement;

public class GameResultManager : MonoBehaviour
{
    public static GameResultManager Instance { get; private set; }

    private int _winner;
    private int _p1Count;
    private int _p2Count;
    private float _clearTime;

    public static float LastTime;
    public static bool NeedsNameInput;

    [Header("�V�[�� & �����[�h�ݒ�")]
    [SerializeField] private float delayBeforeReload = 3f;
    [SerializeField] private string gameSceneName = "Ingame"; // �Q�[���V�[����

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

    /// <summary>
    /// winner: 0 = draw, 1 = P1, 2 = P2
    /// </summary>
    public void SetResult(int winner, int p1, int p2, float time)
    {
        _winner = winner;
        _p1Count = p1;
        _p2Count = p2;
        _clearTime = time;

        LastTime = time;

        bool isSoloMode = TapTwoPlayer.Instance != null && TapTwoPlayer.Instance.IsAI;

        if (isSoloMode)
        {
            // �\�����[�h�F���҂�1�i�v���C���[1�j�̏ꍇ�̂݃����L���O�X�V
            NeedsNameInput = (winner == 1) && RankingSystem.IsNewRecord(time);
        }
        else
        {
            // 2�l�ΐ�F���҂�1��2�Ń����L���O�X�V
            NeedsNameInput = (winner == 1 || winner == 2) && RankingSystem.IsNewRecord(time);
        }

        // �Q�[���V�[���� n �b��Ƀ����[�h
        Invoke(nameof(ReloadGameScene), delayBeforeReload);
    }


    private void ReloadGameScene()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    // ���ʂ��擾���邽�߂�Getter
    public (int winner, int p1, int p2, float time) GetResult() => (_winner, _p1Count, _p2Count, _clearTime);
}
