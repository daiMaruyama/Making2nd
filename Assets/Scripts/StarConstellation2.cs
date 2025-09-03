using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StarConstellation2
{
    public string name;
    public List<Vector3> starPositions;
    public string createdDate;

    public StarConstellation2(string name, List<Vector3> positions)
    {
        this.name = name;
        this.starPositions = positions;
        this.createdDate = System.DateTime.Now.ToString("yyyy/MM/dd");
    }
}

public static class StarCollectionData
{
    public static List<StarConstellation2> constellations = new List<StarConstellation2>();
}
