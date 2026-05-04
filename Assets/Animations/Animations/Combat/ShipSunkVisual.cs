using UnityEngine;

public class ShipSunkVisual : MonoBehaviour
{
    [SerializeField] private Color sunkColor = Color.gray;
    [SerializeField] private SpriteRenderer[] spriteRenderers;

    private bool isMarkedSunk = false;
    private Color[] originalColors;

    private void Awake()
    {
        if (spriteRenderers == null || spriteRenderers.Length == 0)
        {
            spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        }

        originalColors = new Color[spriteRenderers.Length];

        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            if (spriteRenderers[i] != null)
            {
                originalColors[i] = spriteRenderers[i].color;
            }
        }
    }

    public void MarkAsSunk()
    {
        if (isMarkedSunk) return;
        isMarkedSunk = true;

        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            if (spriteRenderers[i] != null)
            {
                spriteRenderers[i].color = sunkColor;
            }
        }
    }

    public void ResetVisual()
    {
        isMarkedSunk = false;

        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            if (spriteRenderers[i] != null)
            {
                spriteRenderers[i].color = originalColors[i];
            }
        }
    }
}