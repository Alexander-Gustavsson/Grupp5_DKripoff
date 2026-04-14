using UnityEngine;

public class PlacementHighlightManager : MonoBehaviour
{
    [SerializeField] private GameObject highlightObject;

    private void Start()
    {
        if (highlightObject != null)
            highlightObject.SetActive(false);
    }

    public void ShowAtWorldPosition(Vector3 worldPosition)
    {
        if (highlightObject == null) return;

        float x = Mathf.Round(worldPosition.x);
        float y = Mathf.Round(worldPosition.y);

        highlightObject.SetActive(true);
        highlightObject.transform.position = new Vector3(x, y, 0f);
    }

    public void Hide()
    {
        if (highlightObject == null) return;

        highlightObject.SetActive(false);
    }
}