using UnityEngine;

public class ShotFeedbackManager : MonoBehaviour
{

    [SerializeField] private GameObject missEffectPrefab;
    [SerializeField] private GameObject hitEffectPrefab;
    [SerializeField] private GameObject sinkEffectPrefab;


    [SerializeField] private BoardShake boardShake;
    [SerializeField] private float hitShakeDuration = 0.12f;
    [SerializeField] private float hitShakeMagnitude = 0.08f;
    [SerializeField] private float sinkShakeDuration = 0.2f;
    [SerializeField] private float sinkShakeMagnitude = 0.14f;

    public void PlayMiss(Vector2 position)
    {
        if (missEffectPrefab != null)
        {
            Instantiate(missEffectPrefab, position, Quaternion.identity);
        }
    }

    public void PlayHit(Vector2 position)
    {
        if (hitEffectPrefab != null)
        {
            Instantiate(hitEffectPrefab, position, Quaternion.identity);
        }

#if UNITY_ANDROID || UNITY_IOS
        Handheld.Vibrate();
#endif

        if (boardShake != null)
        {
            boardShake.Shake(hitShakeDuration, hitShakeMagnitude);
        }
    }

    public void PlaySink(Vector2 position)
    {
        if (sinkEffectPrefab != null)
        {
            Instantiate(sinkEffectPrefab, position, Quaternion.identity);
        }

#if UNITY_ANDROID || UNITY_IOS
        Handheld.Vibrate();
#endif

        if (boardShake != null)
        {
            boardShake.Shake(sinkShakeDuration, sinkShakeMagnitude);
        }
    }
}