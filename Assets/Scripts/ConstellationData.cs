using UnityEngine;

[CreateAssetMenu(menuName = "Constellation Data")]
public class ConstellationData : ScriptableObject
{
    [Header("星座名")]
    public string constellationName;

    [Header("ワールド座標での正しい位置")]
    // public Vector2[] starPositions;

    [Header("ビューポート用に正規化した座標")]
    public Vector2[] starsNormalized;

    [Header("振る回数")]
    public int totalShakes = 100;

    [Header("星の最大数")]
    public int maxCount = 8;

    [Header("生成する星の数")]
    public int starsToGenerate = 8;
}
