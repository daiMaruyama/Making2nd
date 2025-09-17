using UnityEngine;

public class SceneChecker : MonoBehaviour
{
    void Start()
    {
        var t = Object.FindFirstObjectByType<TapTwoPlayer>();
        var g = Object.FindFirstObjectByType<GameUIController>();
        var r = Object.FindFirstObjectByType<GameResultManager>();

        Debug.Log("[SceneChecker] ActiveScene: " + UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        Debug.Log("[SceneChecker] TapTwoPlayer found: " + (t != null) + (t != null ? " active:" + t.gameObject.activeInHierarchy + " enabled:" + (t.enabled) : ""));
        Debug.Log("[SceneChecker] GameUIController found: " + (g != null) + (g != null ? " active:" + g.gameObject.activeInHierarchy + " enabled:" + (g.enabled) : ""));
        Debug.Log("[SceneChecker] GameResultManager found: " + (r != null) + (r != null ? " active:" + r.gameObject.activeInHierarchy : ""));
    }
}
