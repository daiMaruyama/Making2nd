using UnityEngine;

[CreateAssetMenu(menuName = "Constellation Data")]
public class ConstellationData : ScriptableObject
{
    [Header("������")]
    public string constellationName;

    [Header("���[���h���W�ł̐������ʒu")]
    // public Vector2[] starPositions;

    [Header("�r���[�|�[�g�p�ɐ��K���������W")]
    public Vector2[] starsNormalized;

    [Header("�U���")]
    public int totalShakes = 100;

    [Header("���̍ő吔")]
    public int maxCount = 8;

    [Header("�������鐯�̐�")]
    public int starsToGenerate = 8;
}
