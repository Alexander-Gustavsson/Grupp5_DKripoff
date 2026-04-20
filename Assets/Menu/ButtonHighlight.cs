using UnityEngine;
using UnityEngine.UI;

public class ButtonHighlight : MonoBehaviour
{
    [SerializeField] private Color highlightColor;
    private Color baseColor;
    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
        baseColor = image.color;
    }

    public void Highlight()
    {
        if (image.color == highlightColor)
        {
            image.color = baseColor;
        }
        else
        {
            image.color = highlightColor;
        }
    }
}
