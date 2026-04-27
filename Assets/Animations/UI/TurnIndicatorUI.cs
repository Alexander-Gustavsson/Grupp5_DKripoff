using System.Collections;
using UnityEngine;

public class TurnIndicatorUI : MonoBehaviour
{
    [SerializeField] private Transform triangleTransform;
    [SerializeField] private SpriteRenderer triangleRenderer;

    [Header("Colors")]
    [SerializeField] private Color playerColor = new Color(0.3f, 0.9f, 0.7f);
    [SerializeField] private Color enemyColor = new Color(1f, 0.5f, 0.2f);

    [Header("Pulse")]
    [SerializeField] private float pulseDuration = 0.15f;
    [SerializeField] private float pulseScale = 1.2f;

    private Vector3 originalScale;
    private Coroutine pulseRoutine;

    private void Awake()
    {
        if (triangleTransform == null)
        {
            triangleTransform = transform;
        }

        originalScale = triangleTransform.localScale;
    }

    public void ShowPlayerTurn()
    {
        if (triangleTransform != null)
        {
            triangleTransform.localRotation = Quaternion.Euler(0f, 0f, 90f);
        }

        if (triangleRenderer != null)
        {
            triangleRenderer.color = playerColor;
        }

        PlayPulse();
    }

    public void ShowEnemyTurn()
    {
        if (triangleTransform != null)
        {
            triangleTransform.localRotation = Quaternion.Euler(0f, 0f, -90f);
        }

        if (triangleRenderer != null)
        {
            triangleRenderer.color = enemyColor;
        }

        PlayPulse();
    }

    private void PlayPulse()
    {
        if (pulseRoutine != null)
        {
            StopCoroutine(pulseRoutine);
        }

        pulseRoutine = StartCoroutine(PulseRoutine());
    }

    private IEnumerator PulseRoutine()
    {
        float time = 0f;
        Vector3 targetScale = originalScale * pulseScale;

        while (time < pulseDuration)
        {
            float t = time / pulseDuration;
            triangleTransform.localScale = Vector3.Lerp(originalScale, targetScale, t);
            time += Time.deltaTime;
            yield return null;
        }

        time = 0f;
        while (time < pulseDuration)
        {
            float t = time / pulseDuration;
            triangleTransform.localScale = Vector3.Lerp(targetScale, originalScale, t);
            time += Time.deltaTime;
            yield return null;
        }

        triangleTransform.localScale = originalScale;
    }
}