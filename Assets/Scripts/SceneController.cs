using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;        // Fade�p
using System.Collections;

public class SceneController : MonoBehaviour
{
    [Header("�t�F�[�h�ݒ�")]
    [SerializeField] private Image fadePanel;  // ���t�F�[�h�p��Image
    [SerializeField] private float fadeTime = 1.0f;

    [Header("�J�ڐ�ݒ�")]
    [SerializeField] private string nextSceneName;
    [SerializeField] private float waitBeforeFade = 0.5f; // ������Ă���t�F�[�h�J�n�܂ł̑ҋ@����

    bool isTransitioning = false;

    /// <summary>
    /// �O������Ăԁi��F�{�^��OnClick�j
    /// </summary>
    public void StartSceneTransition()
    {
        if (!isTransitioning)
        {
            StartCoroutine(TransitionRoutine());
        }
    }

    private IEnumerator TransitionRoutine()
    {
        isTransitioning = true;

        // �J�ڑO�̑ҋ@
        if (waitBeforeFade > 0)
            yield return new WaitForSeconds(waitBeforeFade);

        // �t�F�[�h���o
        if (fadePanel != null)
        {
            fadePanel.gameObject.SetActive(true);
            Color c = fadePanel.color;
            c.a = 0f;
            fadePanel.color = c;

            float timer = 0f;
            while (timer < fadeTime)
            {
                timer += Time.deltaTime;
                float alpha = Mathf.Clamp01(timer / fadeTime);
                fadePanel.color = new Color(c.r, c.g, c.b, alpha);
                yield return null;
            }
        }

        // �V�[���J��
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
