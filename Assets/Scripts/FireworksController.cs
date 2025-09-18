using UnityEngine;
using UnityEngine.VFX;
using System.Collections;

public class FireworksController : MonoBehaviour
{
    [Header("花火VFX (シーン内に置いてあるものをアタッチ)")]
    [SerializeField] private VisualEffect[] fireworks;
    [SerializeField] private float duration = 2f; // VFX再生時間
    [Header("効果音")]
    [SerializeField] private AudioClip fireworksClip;

    private void Start()
    {
        // すぐにVFX再生＆音再生
        PlayFireworks();

        // 16秒後にもう一度VFXと音を再生
        StartCoroutine(PlayFireworksDelayed(16f));
    }

    private void PlayFireworks()
    {
        foreach (var vfx in fireworks)
        {
            if (vfx == null) continue;
            vfx.Play();
            StartCoroutine(StopVFXAfterSeconds(vfx, duration));
        }

        if (fireworksClip != null)
        {
            AudioSource.PlayClipAtPoint(fireworksClip, Camera.main.transform.position, 0.8f);
        }
    }

    private IEnumerator PlayFireworksDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        PlayFireworks();
    }

    private IEnumerator StopVFXAfterSeconds(VisualEffect vfx, float seconds)
    {
        yield return new WaitForSeconds(seconds);

        if (vfx != null)
        {
            vfx.Stop();
            Destroy(vfx.gameObject);
        }
    }
}
