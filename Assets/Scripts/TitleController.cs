using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.InputSystem; // �VInputSystem

public class TitleController : MonoBehaviour
{
    [Header("�e�L�X�g")]
    [SerializeField] private TextMeshProUGUI leftText;
    [SerializeField] private TextMeshProUGUI centerText;
    [SerializeField] private TextMeshProUGUI rightText;

    [Header("�A�j���[�V�����ݒ�")]
    [SerializeField] private float fadeDuration = 2f;
    [SerializeField] private float delayBetween = 0.5f;

    [Header("��������I�t�Z�b�g")]
    [SerializeField] private Vector3 leftOffset = new Vector3(-100f, 100f, 0f);
    [SerializeField] private Vector3 centerOffset = new Vector3(0f, 100f, 0f);
    [SerializeField] private Vector3 rightOffset = new Vector3(100f, 100f, 0f);

    [Header("�I�u�W�F�N�g")]
    [SerializeField] GameObject _object1;
    [SerializeField] GameObject _object2;
    [SerializeField] GameObject _object3;
    [SerializeField] float moveTime = 2f;


    private bool finished = false;

    private void Awake()
    {
        ResetText(leftText);
        ResetText(centerText);
        ResetText(rightText);
    }

    private void Start()
    {
        Sequence seq = DOTween.Sequence();

        // ���e�L�X�g
        seq.Append(PlayAppear(leftText, leftOffset));

        // �����e�L�X�g
        seq.AppendInterval(delayBetween);
        seq.Append(PlayAppear(centerText, centerOffset));

        // �E�e�L�X�g
        seq.AppendInterval(delayBetween);
        seq.Append(PlayAppear(rightText, rightOffset));

        seq.OnComplete(() => finished = true);
    }

    private void Update()
    {
        if (!finished) return;

        // �VInputSystem - �}�E�X�N���b�N or �^�b�v�����m
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            FadeOutAll();
            finished = false;
        }
        else if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            FadeOutAll();
            finished = false;
        }
    }

    private Tween PlayAppear(TextMeshProUGUI text, Vector3 offset)
    {
        Vector3 startPos = text.transform.localPosition + offset;
        text.transform.localPosition = startPos;

        Sequence appearSeq = DOTween.Sequence();

        appearSeq.Append(text.DOFade(1f, fadeDuration));
        appearSeq.Join(text.transform.DOLocalMove(startPos - offset, fadeDuration).SetEase(Ease.OutQuad));

        return appearSeq;
    }

    private void FadeOutAll()
    {
        leftText.DOFade(0f, 1f);
        centerText.DOFade(0f, 1f);
        rightText.DOFade(0f, 1f);

        if (_object1 != null)
        {
            // �������Ɉړ����ď�����
            _object1.transform.DOLocalMoveX(-Screen.width, moveTime).SetEase(Ease.InQuad);
        }

        if (_object2 != null)
        {
            // �E�����Ɉړ����ď�����
            _object2.transform.DOLocalMoveX(Screen.width, moveTime).SetEase(Ease.InQuad);
        }

        if (_object3 != null)
        {
            // Fade�ŏ�����
            _object3.transform.position = new Vector3(0, 1000, 10000);
        }
    }


    private void ResetText(TextMeshProUGUI text)
    {
        if (text == null) return;
        text.alpha = 0f;
    }
}
