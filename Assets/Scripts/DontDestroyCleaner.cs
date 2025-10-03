using UnityEngine;

public class DontDestroyCleaner : MonoBehaviour
{
    [Header("退避先の親オブジェクト")]
    [SerializeField] private Transform backupParent;

    [Header("退避対象")]
    [SerializeField] private GameObject[] targetObjects;

    /// <summary>
    /// ゲーム終了時に呼ぶ
    /// </summary>
    public void MoveToBackupParent()
    {
        if (backupParent == null)
        {
            Debug.LogWarning("Backup parent is not assigned!");
            return;
        }

        System.Array.ForEach(targetObjects, obj =>
        {
            if (obj != null)
            {
                obj.transform.SetParent(backupParent, true);
                Debug.Log(obj.name + " moved to backup parent.");
            }
        });
    }
}
