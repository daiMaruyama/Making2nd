using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class TapAnimton : MonoBehaviour
{
    [Header("Player 1")]
    [SerializeField] private SpriteRenderer p1Renderer; // �����炠��L������ SpriteRenderer
    [SerializeField] private Sprite p1BlinkSprite;      // �ł�����

    [Header("Player 2")]
    [SerializeField] private SpriteRenderer p2Renderer;
    [SerializeField] private Sprite p2BlinkSprite;

    [SerializeField] private float blinkDuration = 0.2f; // �\������

    private Sprite p1Original;
    private Sprite p2Original;

    // Coroutine �̎Q�Ƃ�ێ����ē��������ŏd�Ȃ�Ȃ��悤�ɂ���
    private Coroutine p1BlinkCoroutine;
    private Coroutine p2BlinkCoroutine;

    private void Start()
    {
        if (p1Renderer != null) p1Original = p1Renderer.sprite;
        if (p2Renderer != null) p2Original = p2Renderer.sprite;
    }

    private void Update()
    {
        // �Q�[�����i�s���łȂ��ꍇ�͊�A�j���[�V�������Ȃ�
        if (TapTwoPlayer.Instance == null || !TapTwoPlayer.Instance.IsRunning) return;

        // Player 1 ����
        if (Keyboard.current.aKey.wasPressedThisFrame || Keyboard.current.dKey.wasPressedThisFrame)
        {
            if (p1Renderer != null)
            {
                if (p1BlinkCoroutine != null) StopCoroutine(p1BlinkCoroutine);
                p1BlinkCoroutine = StartCoroutine(BlinkFace(p1Renderer, p1BlinkSprite, p1Original));
            }
        }

        // Player 2 ����
        if (Keyboard.current.jKey.wasPressedThisFrame || Keyboard.current.lKey.wasPressedThisFrame)
        {
            if (p2Renderer != null)
            {
                if (p2BlinkCoroutine != null) StopCoroutine(p2BlinkCoroutine);
                p2BlinkCoroutine = StartCoroutine(BlinkFace(p2Renderer, p2BlinkSprite, p2Original));
            }
        }
    }

    private IEnumerator BlinkFace(SpriteRenderer renderer, Sprite blink, Sprite original)
    {
        if (renderer == null) yield break;

        renderer.sprite = blink;
        yield return new WaitForSeconds(blinkDuration);
        renderer.sprite = original;
    }
}
