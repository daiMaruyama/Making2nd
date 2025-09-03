using UnityEngine;
using UnityEngine.UI;

public class HomeCollection : MonoBehaviour
{
    public GameObject starPrefab;
    public Transform displayRoot;
    public Text infoText;

    void Start()
    {
        foreach (var constellation in StarCollectionData.constellations)
        {
            foreach (var pos in constellation.starPositions)
            {
                Instantiate(starPrefab, pos, Quaternion.identity, displayRoot);
            }
        }
    }

    // –]‰“‹¾‚Å”`‚¢‚½‚Æ‚«UI•\Ž¦‚·‚é—á
    public void ShowInfo(string name, string date)
    {
        infoText.text = $"{name}\nCreated: {date}";
    }
}
