using UnityEngine;
using UnityEngine.InputSystem;

public class PressToScreen : MonoBehaviour
{
    [Header("共通のシーン遷移コントローラ")]
    [SerializeField] private SceneController transitionController;

    [Header("再生するパーティクル（複数指定可）")]
    [SerializeField] private ParticleSystem[] particleEffects;

    bool isPressed = false;

    private void Start()
    {
        // 起動時にパーティクルを確実に停止・PlayOnAwake無効にしておく
        if (particleEffects != null)
        {
            foreach (var ps in particleEffects)
            {
                if (ps == null) continue;

                // Stopして残った粒子もクリア
                ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

                // PlayOnAwake を無効化（念のため）
                var main = ps.main;
                main.playOnAwake = false;
            }
        }
    }

    void Update()
    {
        if (isPressed) return;

        // 画面タップ or クリック判定
        if ((Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame) ||
            (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame))
        {
            isPressed = true;
            PlayAllParticles();

            // 遷移コントローラがあれば呼ぶ（nullチェック）
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
}
