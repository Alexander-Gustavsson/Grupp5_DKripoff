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

        if (!GuideController.guidesOn)
        {
            Highlight();
            GetComponentInChildren<Lang_Text>().textID = "Guides: Off";
        }
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
