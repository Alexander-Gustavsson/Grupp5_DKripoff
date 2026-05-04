using System.Collections;
using UnityEngine;

public class BoardShake : MonoBehaviour
{
    [SerializeField] private Transform shakeTarget;
    [SerializeField] private float defaultDuration = 0.12f;
    [SerializeField] private float defaultMagnitude = 0.08f;

    private Vector3 originalLocalPosition;
    private Coroutine shakeRoutine;

    private void Awake()
    {
        if (shakeTarget == null)
        {
            shakeTarget = transform;
        }

        originalLocalPosition = shakeTarget.localPosition;
    }

    public void Shake()
    {
        Shake(defaultDuration, defaultMagnitude);
    }

    public void Shake(float duration, float magnitude)
    {
        if (shakeRoutine != null)
        {
            StopCoroutine(shakeRoutine);
        }

        shakeRoutine = StartCoroutine(ShakeRoutine(duration, magnitude));
    }

    private IEnumerator ShakeRoutine(float duration, float magnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            shakeTarget.localPosition = originalLocalPosition + new Vector3(x, y, 0f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        shakeTarget.localPosition = originalLocalPosition;
        shakeRoutine = null;
    }
}