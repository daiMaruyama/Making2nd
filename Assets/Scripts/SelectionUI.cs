using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionUI : MonoBehaviour
{
    [SerializeField] private ConstellationData cassiopeia;
    [SerializeField] private ConstellationData bigDipper;

    public void OnSelectCassiopeia()
    {
        GameManager.Instance.SelectConstellation(cassiopeia);
        SceneManager.LoadScene("InGame");
    }

    public void OnSelectBigDipper()
    {
        GameManager.Instance.SelectConstellation(bigDipper);
        SceneManager.LoadScene("InGame");
    }
}
