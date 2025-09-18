using UnityEngine;
using UnityEngine.InputSystem;

public class PressToScreen : MonoBehaviour
{
    [Header("共通のシーン遷移コントローラ")]
    [SerializeField] private SceneController transitionController;

    [Header("再生するパーティクル（複数指定可）")]
    [SerializeField] private ParticleSystem[] particleEffects;

    [Header("クリック時の効果音")]
    [SerializeField] private AudioClip clickSfx;  // ← 素材をアタッチ
    [SerializeField] private float sfxVolume = 1f;

    bool isPressed = false;

    private void Start()
    {
        if (particleEffects != null)
        {
            foreach (var ps in particleEffects)
            {
                if (ps == null) continue;
                ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                var main = ps.main;
                main.playOnAwake = false;
            }
        }
    }

    void Update()
    {
        if (isPressed) return;

        if ((Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame) ||
            (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame))
        {
            isPressed = true;
            PlayAllParticles();
            PlayClickSfx(); // 👈 ここで音再生

            if (transitionController != null)
                transitionController.StartSceneTransition();
        }
    }

    private void PlayAllParticles()
    {
        if (particleEffects == null) return;

        foreach (var ps in particleEffects)
        {
            if (ps == null) continue;
            if (!ps.gameObject.activeInHierarchy)
                ps.gameObject.SetActive(true);
            ps.Play(true);
        }
    }

    private void PlayClickSfx()
    {
        if (clickSfx == null) return;
        AudioSource.PlayClipAtPoint(clickSfx, Camera.main.transform.position, sfxVolume);
    }
}
