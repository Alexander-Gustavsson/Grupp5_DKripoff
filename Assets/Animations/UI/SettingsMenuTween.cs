using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SettingsMenuTween : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CanvasGroup overlayGroup;
    [SerializeField] private RectTransform panel;
    [SerializeField] private Button openButton;
    [SerializeField] private Button closeButton;

    [Header("Positions")]
    [SerializeField] private Vector2 hiddenPosition = new Vector2(0f, -1200f);
    [SerializeField] private Vector2 shownPosition = Vector2.zero;

    [Header("Timing")]
    [SerializeField] private float overlayFadeDuration = 0.2f;
    [SerializeField] private float panelMoveDuration = 0.3f;
    [SerializeField] private float panelScaleDuration = 0.25f;

    private bool isOpen = false;

    private void Start()
    {
        if (openButton != null)
            openButton.onClick.AddListener(OpenMenu);

        if (closeButton != null)
            closeButton.onClick.AddListener(CloseMenu);

        if (overlayGroup != null)
        {
            overlayGroup.alpha = 0f;
            overlayGroup.interactable = false;
            overlayGroup.blocksRaycasts = false;
        }

        if (panel != null)
        {
            panel.anchoredPosition = hiddenPosition;
            panel.localScale = Vector3.one * 0.95f;
        }
    }

    public void OpenMenu()
    {
        if (isOpen) return;
        isOpen = true;

        if (overlayGroup != null)
        {
            overlayGroup.DOKill();
            overlayGroup.alpha = 0f;
            overlayGroup.interactable = true;
            overlayGroup.blocksRaycasts = true;
            overlayGroup.DOFade(1f, overlayFadeDuration);
        }

        if (panel != null)
        {
            panel.DOKill();
            panel.anchoredPosition = hiddenPosition;
            panel.localScale = Vector3.one * 0.95f;

            Sequence seq = DOTween.Sequence();
            seq.Append(panel.DOAnchorPos(shownPosition, panelMoveDuration).SetEase(Ease.OutCubic));
            seq.Join(panel.DOScale(1f, panelScaleDuration).SetEase(Ease.OutBack));
        }

        if (openButton != null)
        {
            openButton.transform.DOKill();
            openButton.transform.DOPunchScale(Vector3.one * 0.1f, 0.2f, 4, 0.5f);
        }
    }

    public void CloseMenu()
    {
        if (!isOpen) return;
        isOpen = false;

        if (overlayGroup != null)
        {
            overlayGroup.DOKill();
            overlayGroup.DOFade(0f, overlayFadeDuration).OnComplete(() =>
            {
                overlayGroup.interactable = false;
                overlayGroup.blocksRaycasts = false;
            });
        }

        if (panel != null)
        {
            panel.DOKill();

            Sequence seq = DOTween.Sequence();
            seq.Append(panel.DOAnchorPos(hiddenPosition, panelMoveDuration).SetEase(Ease.InCubic));
            seq.Join(panel.DOScale(0.95f, panelScaleDuration).SetEase(Ease.InBack));
        }

        if (closeButton != null)
        {
            closeButton.transform.DOKill();
            closeButton.transform.DOPunchScale(Vector3.one * 0.1f, 0.2f, 4, 0.5f);
        }
    }
}