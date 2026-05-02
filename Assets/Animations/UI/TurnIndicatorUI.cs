using System.Collections;
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

    //[Header("Rotations")]
    //[SerializeField] private Vector3 playerRotation = new Vector3(0f, 0f, 90f);
    //[SerializeField] private Vector3 enemyRotation = new Vector3(0f, 0f, -90f);

    [Header("Pulse")]
    [SerializeField] private float pulseDuration = 0.15f;
    [SerializeField] private float pulseScale = 1.2f;

    private Vector3 originalScale;
    private Coroutine pulseRoutine;

    private void Awake()
    {
        //if (triangleTransform == null)
        //{
        //    triangleTransform = transform;
        //}

        originalScale = transform.localScale;
    }

    public void ShowPlayerTurn()

    {
        transform.position = playerPosition;

        if (playerTurnObject != null) playerTurnObject.SetActive(true);
        if (enemyTurnObject != null) enemyTurnObject.SetActive(false);

        PlayPulse();

    }

    public void ShowEnemyTurn()
    {
        transform.position = enemyPosition;

        if (playerTurnObject != null) playerTurnObject.SetActive(false);
        if (enemyTurnObject != null) enemyTurnObject.SetActive(true);

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
            transform.localScale = Vector3.Lerp(originalScale, targetScale, t);
            time += Time.deltaTime;
            yield return null;
        }

        time = 0f;
        while (time < pulseDuration)
        {
            float t = time / pulseDuration;
            transform.localScale = Vector3.Lerp(targetScale, originalScale, t);
            time += Time.deltaTime;
            yield return null;
        }
        transform.localScale = originalScale;
    }
}