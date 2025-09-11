using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectMode : MonoBehaviour
{
    [Header("�V�[����")]
    [SerializeField] string versusSceneName = "IngameVersus";
    [SerializeField] string ghostSceneName = "IngameGhost";
    
    public void OnVersusMode()
    {
        SceneManager.LoadScene(versusSceneName);
    }

    public void OnGhostMode()
    {
        SceneManager.LoadScene(ghostSceneName);
    }
}
