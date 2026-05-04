using UnityEngine;

public class GridPlacementHighlightManager : MonoBehaviour
{
    [SerializeField] private GameObject[] highlightSquares;

    private void Start()
    {
        HideAll();
    }

    private void Update()
    {
        DragDrop draggingShip = FindDraggingShip();

        if (draggingShip == null)
        {
            HideAll();
            return;
        }

        ShowShipHighlights(draggingShip);
    }

    private DragDrop FindDraggingShip()
    {
        DragDrop[] ships = FindObjectsByType<DragDrop>(FindObjectsSortMode.None);

        foreach (DragDrop ship in ships)
        {
            if (ship.dragging)
                return ship;
        }

        return null;
    }

    private void ShowShipHighlights(DragDrop draggingShip)
    {
        Vector3 rootPos = draggingShip.transform.position;
        float snappedX = Mathf.Round(rootPos.x);
        float snappedY = Mathf.Round(rootPos.y);

        bool insideGrid = snappedX >= 0.5f && snappedX <= 8.5f &&
                          snappedY >= 0.5f && snappedY <= 8.5f;

        if (!insideGrid)
        {
            HideAll();
            return;
        }
        HideAll();

        int length = Mathf.Clamp(draggingShip.ShipLength, 1, highlightSquares.Length);

       

        float z = Mathf.Round(draggingShip.transform.eulerAngles.z) % 360;
        bool vertical = z == 0 || z == 180;

        for (int i = 0; i < length; i++)
        {
            if (highlightSquares[i] == null) continue;

            float offset = i - (length -1) / 2f;

            float x = rootPos.x;
            float y = rootPos.y;

            if (vertical)
                y -= offset;
            else
                x += offset;

            x = Mathf.Round(x);
            y = Mathf.Round(y);

            highlightSquares[i].SetActive(true);
            // highlightSquares[i].transform.position = new Vector3(x, y, 0f);
            highlightSquares[i].transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y) + 0f, 0f);
        }
    }

    private void HideAll()
    {
        foreach (GameObject square in highlightSquares)
        {
            if (square != null)
                square.SetActive(false);
        }
    }
}