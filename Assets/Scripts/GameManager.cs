using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { Shaking, Connecting }

public class GameManager : MonoBehaviour
{
    public GameState currentState = GameState.Shaking;
    public int maxShakeCount = 5;

    public LineDrawer lineDrawer;

    private void Update()
    {
        if (currentState == GameState.Shaking)
        {
            if (ShakenStarData.positions.Count >= maxShakeCount)
            {
                currentState = GameState.Connecting;
                StartConnecting();
            }
        }
    }

    void StartConnecting()
    {
        lineDrawer.DrawLine(ShakenStarData.positions);
        // 成功判定は即成功に簡略化
        SaveConstellation();
    }

    void SaveConstellation()
    {
        StarCollectionData.constellations.Add(
            new StarConstellation2("MyConstellation", new List<Vector3>(ShakenStarData.positions))
        );

        // 次のシーンへ（ホームコレクション）
        SceneManager.LoadScene("HomeScene");
    }
}
