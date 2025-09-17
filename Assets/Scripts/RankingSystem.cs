using UnityEngine;

public static class RankingSystem
{
    private const int MaxRank = 5;

    public static bool IsNewRecord(float newTime)
    {
        for (int i = 0; i < MaxRank; i++)
        {
            float oldTime = PlayerPrefs.GetFloat($"BestTime{i}", float.MaxValue);
            if (newTime < oldTime) return true;
        }
        return false;
    }

    public static void SaveRecord(string playerName, float newTime)
    {
        float[] times = new float[MaxRank];
        string[] names = new string[MaxRank];

        for (int i = 0; i < MaxRank; i++)
        {
            times[i] = PlayerPrefs.GetFloat($"BestTime{i}", float.MaxValue);
            names[i] = PlayerPrefs.GetString($"BestName{i}", "---");
        }

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
            for (int j = MaxRank - 1; j > insertIndex; j--)
            {
                times[j] = times[j - 1];
                names[j] = names[j - 1];
            }

            times[insertIndex] = newTime;
            names[insertIndex] = playerName;

            for (int i = 0; i < MaxRank; i++)
            {
                PlayerPrefs.SetFloat($"BestTime{i}", times[i]);
                PlayerPrefs.SetString($"BestName{i}", names[i]);
            }
            PlayerPrefs.Save();
        }
    }

    public static (string name, float time)[] GetRanking()
    {
        var ranking = new (string, float)[MaxRank];
        for (int i = 0; i < MaxRank; i++)
        {
            float time = PlayerPrefs.GetFloat($"BestTime{i}", float.MaxValue);
            string name = PlayerPrefs.GetString($"BestName{i}", "---");
            ranking[i] = (name, time);
        }
        return ranking;
    }
}
