using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public ConstellationData selectedConstellation;
    public int totalShakes = 100;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SelectConstellation(ConstellationData constellation)
    {
        selectedConstellation = constellation;
        totalShakes = constellation.totalShakes;
    }

}
