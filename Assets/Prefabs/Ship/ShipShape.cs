using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class ShipShape : MonoBehaviour
{
    //[SerializeField] private int shipPoints;

    List<Vector2> shipPos = new List<Vector2>();

    public void ShipPlaced()
    {

        Bounds shipBounds = GetComponent<Collider2D>().bounds;


        int minX = Mathf.RoundToInt(shipBounds.min.x);
        int maxX = Mathf.RoundToInt(shipBounds.max.x);
        int minY = Mathf.RoundToInt(shipBounds.min.y);
        int maxY = Mathf.RoundToInt(shipBounds.max.y);

        for (int x = minX; x <= maxX; x++)
        {
            for (int y = minY; y <= maxY; y++)
            {
                shipPos.Add(new Vector2(x, y));
            }
        }
    }

    public bool IsShipGone()
    {
        if (shipPos.Count == 0)
        {
            return true;
        }
        return false;
    }

    public bool IsShipHit(Vector2 pos)
    {
        if (shipPos.Contains(pos))
        {
            shipPos.Remove(pos);
            return true;
        }
        return false;
    }

}
