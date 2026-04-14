using UnityEngine;

public class TileHighlight : MonoBehaviour
{
    [SerializeField] private GameObject highlightObject;

    private void Start()
    {
        if (highlightObject != null)
            highlightObject.SetActive(false);
    }

    public void ShowHighlight(Vector2 gridPosition)
    {
        if (highlightObject == null) return;

        highlightObject.SetActive(true);
        highlightObject.transform.position = new Vector3(gridPosition.x, gridPosition.y, 0f);
    }

    public void HideHighlight()
    {
        if (highlightObject == null) return;

        highlightObject.SetActive(false);
    }
}