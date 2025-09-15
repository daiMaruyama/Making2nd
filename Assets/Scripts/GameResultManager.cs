using UnityEngine;
using UnityEngine.SceneManagement;

public class GameResultManager : MonoBehaviour
{
    public static GameResultManager Instance { get; private set; }

    private int _winner;
    private int _p1Count;
    private int _p2Count;
    private float _clearTime;

    [SerializeField] private float delayBeforeSceneChange = 3f;
    [SerializeField] private string sceneName;

    // �����L���O�X�V�t���O�iResultScene���Q�Ɓj
    private bool _lastWasNewRecord = false;
    private int _lastNewRecordIndex = -1;

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

        // Reset flags
        _lastWasNewRecord = false;
        _lastNewRecordIndex = -1;

        // ���҂�����ꍇ�̂݃����L���O����i�������w�W�j
        if (winner == 1 || winner == 2)
        {
            SaveRecord(time);
        }

        // �� �C���Q�[���ł͏��҉��o�͍s��Ȃ��iResultScene �ŕ\�����邽�߁j
        // ResultScene �֑J��
        Invoke(nameof(GoToResultScene), delayBeforeSceneChange);
    }

    private void SaveRecord(float newTime)
    {
        const int MaxRank = 5;

        float[] times = new float[MaxRank];
        for (int i = 0; i < MaxRank; i++)
        {
            times[i] = PlayerPrefs.GetFloat($"BestTime{i}", float.MaxValue);
        }

        // �}���ʒu��T���i�������^�C������ʁj
        int insertIndex = -1;
        for (int i = 0; i < MaxRank; i++)
        {
            if (newTime < times[i])
            {
                insertIndex = i;
                break;
            }
        }

        if (insertIndex != -1)
        {
            // �}�����Č����V�t�g
            for (int j = MaxRank - 1; j > insertIndex; j--)
            {
                times[j] = times[j - 1];
            }
            times[insertIndex] = newTime;

            // �ۑ�
            for (int i = 0; i < MaxRank; i++)
            {
                PlayerPrefs.SetFloat($"BestTime{i}", times[i]);
            }
            PlayerPrefs.Save();

            // �t���O���Z�b�g�iResultScene �Ŗ��O���͂Ȃǂ��s�����߁j
            _lastWasNewRecord = true;
            _lastNewRecordIndex = insertIndex;
        }
        else
        {
            // �V�L�^�ɓ���Ȃ������ꍇ�͉������Ȃ��i�����̃����L���O�ێ��j
            _lastWasNewRecord = false;
            _lastNewRecordIndex = -1;
        }
    }

    private void GoToResultScene()
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogWarning("GameResultManager: sceneName is not set.");
            return;
        }
        SceneManager.LoadScene(sceneName);
    }

    // ResultScene ����Ăׂ� getter
    public (int winner, int p1, int p2, float time) GetResult()
    {
        return (_winner, _p1Count, _p2Count, _clearTime);
    }

    // ResultScene ���Q�Ƃ��āu���O���͂��K�v���v���m�F���邽�߂�API
    public bool LastResultWasNewRecord() => _lastWasNewRecord;
    public int GetLastNewRecordIndex() => _lastNewRecordIndex;
}
