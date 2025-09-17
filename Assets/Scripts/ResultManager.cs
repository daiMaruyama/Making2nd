using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class ResultManager : MonoBehaviour
{
    [Header("シーン名")]
    [SerializeField] private string _gameSceneName;
    [SerializeField] private string _titleSceneName;

    [Header("UI Image")]
    [SerializeField] private Image _gameImage;
    [SerializeField] private Image _titleImage;

    [Header("ホールド秒数")]
    [SerializeField] private float _requiredHoldTime = 2f;

    [Header("ランキング入力UI")]
    [SerializeField] private TMP_InputField _nameInput;
    [SerializeField] private GameObject _inputPanel;

    private float _holdTime1 = 0f;
    private float _holdTime2 = 0f;

    private void Start()
    {
        // ResetAllPrefs();
        if (GameResultManager.Instance == null)
        {
            Debug.LogError("GameResultManager が存在しません！");
            _inputPanel.SetActive(false);
            return;
        }

        // GameResultManager で更新対象なら入力パネルを開く
        _inputPanel.SetActive(GameResultManager.NeedsNameInput);
    }

    public void ResetAllPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("PlayerPrefs をリセットしました。");
    }

    void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        // --- 1P: A or Dでタイトル ---
        if (keyboard.aKey.isPressed || keyboard.dKey.isPressed)
        {
            _holdTime1 += Time.deltaTime;
            _titleImage.fillAmount = Mathf.Clamp01(_holdTime1 / _requiredHoldTime);

            if (_holdTime1 >= _requiredHoldTime)
                SceneManager.LoadScene(_titleSceneName);
        }
        else
        {
            _holdTime1 = 0f;
            _titleImage.fillAmount = 0f;
        }

        // --- 2P: J or LでInGame ---
        if (keyboard.jKey.isPressed || keyboard.lKey.isPressed)
        {
            _holdTime2 += Time.deltaTime;
            _gameImage.fillAmount = Mathf.Clamp01(_holdTime2 / _requiredHoldTime);

            if (_holdTime2 >= _requiredHoldTime)
                SceneManager.LoadScene(_gameSceneName);
        }
        else
        {
            _holdTime2 = 0f;
            _gameImage.fillAmount = 0f;
        }
    }

    // 名前入力決定ボタン
    public void OnSubmitName()
    {
        string playerName = _nameInput.text.Trim();
        if (string.IsNullOrWhiteSpace(playerName)) playerName = "NoName";

        // 最後のタイムをランキングに保存
        RankingSystem.SaveRecord(playerName, GameResultManager.LastTime);

        _inputPanel.SetActive(false);
    }

    // スキップ用
    public void OnSkip()
    {
        _inputPanel.SetActive(false);
    }
}
