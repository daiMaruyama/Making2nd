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
        // ��������͑������Ɋȗ���
        SaveConstellation();
    }

    void SaveConstellation()
    {
        StarCollectionData.constellations.Add(
            new StarConstellation2("MyConstellation", new List<Vector3>(ShakenStarData.positions))
        );

        // ���̃V�[���ցi�z�[���R���N�V�����j
        SceneManager.LoadScene("HomeScene");
    }
}
