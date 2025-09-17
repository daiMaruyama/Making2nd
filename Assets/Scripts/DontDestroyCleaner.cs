using UnityEngine;

public class DontDestroyCleaner : MonoBehaviour
{
    [Header("�ޔ��̐e�I�u�W�F�N�g")]
    [SerializeField] private Transform backupParent;

    [Header("�ޔ�Ώ�")]
    [SerializeField] private GameObject[] targetObjects;

    /// <summary>
    /// �Q�[���I�����ɌĂ�
    /// </summary>
    public void MoveToBackupParent()
    {
        if (backupParent == null)
        {
            Debug.LogWarning("Backup parent is not assigned!");
            return;
        }

        foreach (var obj in targetObjects)
        {
            if (obj != null)
            {
                obj.transform.SetParent(backupParent, true); // true = ���[���h���W�ێ�
                Debug.Log(obj.name + " moved to backup parent.");
            }
        }
    }
}
