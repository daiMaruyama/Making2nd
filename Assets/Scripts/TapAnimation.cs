using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class TapAnimton : MonoBehaviour
{
    [Header("Player 1")]
    [SerializeField] private SpriteRenderer p1Renderer;
    [SerializeField] private Sprite p1BlinkSprite;

    [Header("Player 2")]
    [SerializeField] private SpriteRenderer p2Renderer;
    [SerializeField] private Sprite p2BlinkSprite;

    [SerializeField] private float blinkDuration = 0.2f;

    private Sprite p1Original;
    private Sprite p2Original;

    private Coroutine p1BlinkCoroutine;
    private Coroutine p2BlinkCoroutine;

    private int _lastPlayer2Count = 0;

    private void Start()
    {
        if (p1Renderer != null) p1Original = p1Renderer.sprite;
        if (p2Renderer != null) p2Original = p2Renderer.sprite;
    }

    private void Update()
    {
        if (TapTwoPlayer.Instance == null || !TapTwoPlayer.Instance.IsRunning) return;

        // Player1 入力
        if (Keyboard.current.aKey.wasPressedThisFrame || Keyboard.current.dKey.wasPressedThisFrame)
        {
            StartBlink(p1Renderer, p1BlinkSprite, p1Original, ref p1BlinkCoroutine);
        }

        // Player2 入力
        if (!TapTwoPlayer.Instance.IsAI)
        {
            if (Keyboard.current.jKey.wasPressedThisFrame || Keyboard.current.lKey.wasPressedThisFrame)
            {
                StartBlink(p2Renderer, p2BlinkSprite, p2Original, ref p2BlinkCoroutine);
            }
        }
        else
        {
            // AIの場合：カウント増加を検知してアニメ
            int currentCount = TapTwoPlayer.Instance.Player2Count;
            if (currentCount > _lastPlayer2Count)
            {
                StartBlink(p2Renderer, p2BlinkSprite, p2Original, ref p2BlinkCoroutine);
                _lastPlayer2Count = currentCount;
            }
        }
    }

    private void StartBlink(SpriteRenderer renderer, Sprite blink, Sprite original, ref Coroutine coroutine)
    {
        if (renderer == null) return;
        if (coroutine != null) StopCoroutine(coroutine);
        coroutine = StartCoroutine(BlinkFace(renderer, blink, original));
    }

    private IEnumerator BlinkFace(SpriteRenderer renderer, Sprite blink, Sprite original)
    {
        if (renderer == null) yield break;

        renderer.sprite = blink;
        yield return new WaitForSeconds(blinkDuration);
        renderer.sprite = original;
    }
}
