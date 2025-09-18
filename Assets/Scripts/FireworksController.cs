using UnityEngine;
using UnityEngine.VFX;
using System.Collections;

public class FireworksController : MonoBehaviour
{
    [Header("�ԉ�VFX (�V�[�����ɒu���Ă�����̂��A�^�b�`)")]
    [SerializeField] private VisualEffect[] fireworks;
    [SerializeField] private float duration = 2f; // VFX�Đ�����
    [Header("���ʉ�")]
    [SerializeField] private AudioClip fireworksClip;

    private void Start()
    {
        // ������VFX�Đ������Đ�
        PlayFireworks();

        // 16�b��ɂ�����xVFX�Ɖ����Đ�
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
