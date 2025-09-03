using System.Collections;
using UnityEngine;

public class StarController : MonoBehaviour
{
    public bool IsShaken { get; private set; } = false;

    public void Shake()
    {
        if (IsShaken) return;
        IsShaken = true;

        // �ȈՃA�j���[�V����
        StartCoroutine(ShakeAnimation());

        // �U�������W��ۑ�
        ShakenStarData.positions.Add(transform.position);
    }

    private IEnumerator ShakeAnimation()
    {
        Vector3 original = transform.position;
        transform.position += new Vector3(Random.Range(-0.1f, 0.1f), 0.3f, 0);
        yield return new WaitForSeconds(0.2f);
        transform.position = original;
    }
}
