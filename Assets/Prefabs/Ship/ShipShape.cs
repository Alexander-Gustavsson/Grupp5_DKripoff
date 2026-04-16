using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class ShipShape : MonoBehaviour
{
    [SerializeField] private int shipPoints;

    List<Vector2> shipPos = new List<Vector2>();

    public void ShipPlaced()
    {
        shipPos.Clear();
        Vector3 direction = transform.up == Vector3.up ? Vector3.up : transform.up == Vector3.down ? Vector3.down : transform.up == Vector3.right ? Vector3.right : Vector3.left;

        for (int i = 0; i < shipPoints; i++)
        {
            shipPos.Add(transform.position + direction * i);
        }
    }

    public List<Vector2> GetShipPositions()
    {
        return shipPos;
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