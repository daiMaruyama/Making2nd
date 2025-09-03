using UnityEngine;

[CreateAssetMenu(menuName = "Constellation Data")]
public class ConstellationData : ScriptableObject
{
    public string constellationName;    // ������
    public Vector2[] starPositions;     // ���[���h���W�ł̐������ʒu
    public Vector2[] starsNormalized;   // �r���[�|�[�g�p�ɐ��K���������W
    public int totalShakes = 100;       // �U���
    public int maxCount = 8;            // ���̍ő吔
    public int starsToGenerate = 8;     // �������鐯�̐�
}
