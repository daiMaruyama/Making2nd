using UnityEngine;
using TMPro;

public class RankingDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text[] rankTexts = new TMP_Text[5];

    private void OnEnable()
    {
        UpdateRanking();
    }

    public void UpdateRanking()
    {
        for (int i = 0; i < rankTexts.Length; i++)
        {
            float time = PlayerPrefs.GetFloat($"BestTime{i}", float.MaxValue);
            string name = PlayerPrefs.GetString($"BestName{i}", "----");

            if (time == float.MaxValue)
                rankTexts[i].text = $"{i + 1}. {name} : ---";
            else
                rankTexts[i].text = $"{i + 1}. {name} : {time:0.00}s";
        }
    }
}
