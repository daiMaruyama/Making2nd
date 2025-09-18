using UnityEngine;
using UnityEngine.VFX;
using System.Collections;

public class FireworksController : MonoBehaviour
{
    [Header("花火VFX (シーン内に置いてあるものをアタッチ)")]
    [SerializeField] private VisualEffect[] fireworks;
    [SerializeField] private float duration = 2f; // 秒数はInspectorで調整

    private void Start()
    {
        // 配列に入れたすべての VFX を再生
        foreach (var vfx in fireworks)
        {
            if (vfx == null) continue;
            vfx.Play();
            StartCoroutine(StopVFXAfterSeconds(vfx, duration));
        }
    }

    private IEnumerator StopVFXAfterSeconds(VisualEffect vfx, float seconds)
    {
        yield return new WaitForSeconds(seconds);

        if (vfx != null)
        {
            vfx.Stop();
            Destroy(vfx.gameObject);   // 安全に破棄
        }
    }
}
