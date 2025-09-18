using UnityEngine;
using UnityEngine.VFX;
using System.Collections;

public class FireworksController : MonoBehaviour
{
    [Header("�ԉ�VFX (�V�[�����ɒu���Ă�����̂��A�^�b�`)")]
    [SerializeField] private VisualEffect[] fireworks;
    [SerializeField] private float duration = 2f; // �b����Inspector�Œ���

    private void Start()
    {
        // �z��ɓ��ꂽ���ׂĂ� VFX ���Đ�
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
            Destroy(vfx.gameObject);   // ���S�ɔj��
        }
    }
}
