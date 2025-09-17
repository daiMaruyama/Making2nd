using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneInit : MonoBehaviour
{
    void Awake()
    {
        Debug.Log("[SceneInit] Awake - resetting timescale and DOTween");
        Time.timeScale = 1f;

#if USE_DOTWEEN
        DG.Tweening.DOTween.KillAll();
#endif
    }
}
