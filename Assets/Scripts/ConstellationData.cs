using UnityEngine;

[CreateAssetMenu(fileName = "ConstellationData", menuName = "Constellation/StarData")]
public class ConstellationData : ScriptableObject
{
    public string constellationName;
    public int maxCount;        // ¯‚ÌÅ‘å
    public int starsToGenerate; // ÀÛ‚É¶¬‚·‚é¯‚Ì”
}