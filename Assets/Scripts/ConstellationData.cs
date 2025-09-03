using UnityEngine;

[CreateAssetMenu(menuName = "Constellation Data")]
public class ConstellationData : ScriptableObject
{
    public string constellationName;    // 星座名
    public Vector2[] starPositions;     // ワールド座標での正しい位置
    public Vector2[] starsNormalized;   // ビューポート用に正規化した座標
    public int totalShakes = 100;       // 振る回数
    public int maxCount = 8;            // 星の最大数
    public int starsToGenerate = 8;     // 生成する星の数
}
