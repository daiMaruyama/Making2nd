using UnityEngine;
using UnityEngine.VFX;

public class VFXSpawner : MonoBehaviour
{
    [SerializeField] VisualEffect _vfxPrehub;
    [SerializeField] Transform _startTransform;
    [SerializeField] Transform _endTransform;
    [SerializeField] int _count = 5;

    void Start()
    {
        SpawnVFX();
    }

    void SpawnVFX()
    {
        for (int i = 0; i < _count; i++)
        {
            Vector3 randomPos = Vector3.Lerp(_startTransform.position, _endTransform.position, Random.value);
            VisualEffect _vfx = Instantiate(_vfxPrehub, randomPos, Quaternion.identity);
            if (_vfx.HasVector3("randomPos"))
            {
                _vfx.SetVector3("randomPos", randomPos);
            }
        }
    }
}
