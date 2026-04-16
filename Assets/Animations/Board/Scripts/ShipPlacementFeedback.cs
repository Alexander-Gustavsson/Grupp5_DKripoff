using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ShipPlacementFeedback : MonoBehaviour
{
    [SerializeField] private Color validColor = Color.green;
    [SerializeField] private Color invalidColor = Color.red;
    //[SerializeField] private Color selectedColor = Color.yellow;

    private SpriteRenderer sr;
    private Color originalColor;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }

    //public void ShowSelected()
    //{
    //    sr.color = selectedColor;
    //}

    public void ShowValid()
    {
        sr.color = validColor;
    }

    public void ShowInvalid()
    {
        sr.color = invalidColor;
    }

    public void ResetFeedback()
    {
        sr.color = originalColor;
    }
}
