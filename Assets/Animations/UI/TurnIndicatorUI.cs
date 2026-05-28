using DG.Tweening;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TurnIndicatorUI : MonoBehaviour
{
    [SerializeField] private GameObject playerTurnObject;
    [SerializeField] private GameObject enemyTurnObject;

    [Header("Positions")]
    [SerializeField] private Vector3 playerPosition;
    [SerializeField] private Vector3 enemyPosition;

    [Header("Colors")]
    [SerializeField] private Color playerColor = new Color(0.3f, 0.9f, 0.7f);
    [SerializeField] private Color enemyColor = new Color(1f, 0.5f, 0.2f);


    [Header("Turn Text")]
    [SerializeField] private CanvasGroup turnTextGroup;
    [SerializeField] private TMP_Text turnText;

    [Header("Board Glow")]
    [SerializeField] private SpriteRenderer playerBoardGlow;
    [SerializeField] private SpriteRenderer enemyBoardGlow;
    [SerializeField] private float glowAlpha = 0.5f;


    [Header("Tween")]

    [SerializeField] private float moveDuratation = 0.25f;
    [SerializeField] private float pulseDuration = 0.15f;
    [SerializeField] private float pulseScale = 1.2f;
    // [SerializeField] private float textShowTime = 0.25f;

    private Vector3 originalScale;
    private Tween moveTween;
    private Tween pulseTween;
    private bool isPlayerTurn = false;
    private bool firstShow = true;


    private void Awake()
    {
        originalScale = transform.localScale;

        if (turnTextGroup != null)
        {
            turnTextGroup.alpha = 0f;

        }
        if (playerBoardGlow != null)
        {
            Color c = playerBoardGlow.color;
            c.a = 0f;
            playerBoardGlow.color = c;
        }

        if (enemyBoardGlow != null)
        {
            Color c = enemyBoardGlow.color;
            c.a = 0f;
            enemyBoardGlow.color = c;
        }
    }

    public void ShowPlayerTurn()

    {
        if (!firstShow && isPlayerTurn)
        {
            return;
        }

        firstShow = false;
        isPlayerTurn = true;
        PlayMove(playerPosition);


        if (playerTurnObject != null) playerTurnObject.SetActive(true);
        if (enemyTurnObject != null) enemyTurnObject.SetActive(false);

        transform.DOKill();
        transform.position = transform.position;
        transform.DOMove(playerPosition, moveDuratation).SetEase(Ease.InOutSine);
        transform.localScale = originalScale;
        transform.DOPunchScale(Vector3.one * (pulseScale - 1f), pulseDuration, 4, 0.5f);

        ShowTurnText("PLAYER TURN", playerColor);
        ShowBoardGlow(playerBoardGlow, enemyBoardGlow);


        PlayPulse();

    }

    public void ShowEnemyTurn()
    {
        if (!firstShow && !isPlayerTurn)
        {
            return;
        }
        firstShow = false;
        isPlayerTurn = false;
        PlayMove(enemyPosition);

        if (playerTurnObject != null) playerTurnObject.SetActive(false);
        if (enemyTurnObject != null) enemyTurnObject.SetActive(true);

        transform.DOKill();
        transform.position = transform.position;
        transform.DOMove(playerPosition, moveDuratation).SetEase(Ease.InOutSine);
        transform.localScale = originalScale;
        transform.DOPunchScale(Vector3.one * (pulseScale - 1f), pulseDuration, 4, 0.5f);

        ShowTurnText("ENEMY TURN", enemyColor);
        ShowBoardGlow(enemyBoardGlow, playerBoardGlow);
        PlayPulse();
    }

    private void ShowTurnText(string text, Color color)
    {
        if (turnTextGroup == null || turnText == null) return;

        turnTextGroup.DOKill();
        turnText.transform.DOKill();

        turnText.text = text;
        turnText.color = color;
        turnTextGroup.alpha = 0f;
        turnText.transform.localScale = Vector3.one * 0.8f;

        DG.Tweening.Sequence seq = DOTween.Sequence();
        seq.Append(turnTextGroup.DOFade(1f, 0.15f));
        seq.Join(turnText.transform.DOScale(1f, 0.15f));
        //seq.AppendInterval(textShowTime);
        //seq.Append(turnTextGroup.DOFade(0f, 0.25f));
    }
    private void ShowBoardGlow(SpriteRenderer activeGlow, SpriteRenderer inactiveGlow)
    {
        if (activeGlow != null)
        {
            activeGlow.DOKill();
            Color c = activeGlow.color;
            c.a = 0f;
            activeGlow.color = c;

            // activeGlow.DOFade(glowAlpha, 0.18f).SetLoops(2, LoopType.Yoyo);
            DG.Tweening.Sequence glowSeq = DOTween.Sequence();
            glowSeq.Append(activeGlow.DOFade(glowAlpha + 0.08f, 0.12f));
            glowSeq.Append(activeGlow.DOFade(glowAlpha, 0.18f));
        }
        if (inactiveGlow != null)
        {
            inactiveGlow.DOKill();
            inactiveGlow.DOFade(0f, 0.1f);
        }
    }

    public void PlayMove(Vector3 targetPosition)
    {
        moveTween?.Kill();
        moveTween = transform.DOMove(targetPosition, moveDuratation).SetEase(Ease.InOutSine);
    }

    private void PlayPulse()
    {

        pulseTween?.Kill();
        transform.localScale = originalScale;

        pulseTween = transform.DOPunchScale(Vector3.one * (pulseScale - 1f), pulseDuration, 4, 0.5f);
    }


}