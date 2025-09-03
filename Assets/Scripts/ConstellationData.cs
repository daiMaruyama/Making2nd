using UnityEngine;

[CreateAssetMenu(fileName = "ConstellationData", menuName = "Constellation/StarData")]
public class ConstellationData : ScriptableObject
{
    public string constellationName;
    public int maxCount;        // ���̍ő�
    public int starsToGenerate; // ���ۂɐ������鐯�̐�
}