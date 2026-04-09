using System.Collections.Generic;
// using UnityEditor;
using UnityEngine;
// using static UnityEditor.PlayerSettings;

public class AI : MonoBehaviour
{
    [SerializeField] private GameObject[] ships;

    List<Vector2> guessed = new List<Vector2>();


    // Kan lägga till om skeppen ska kunna roteras senare
    public void PlaceShips()
    {
        foreach (GameObject ship in ships)
        {
            Vector2 pos = RandomPosition();

            if (ShipOnPosition(pos) == null)
            {
                ship.transform.position = pos;
            }

            ship.SetActive(false);
        }
    }

    public bool TakeHit(Vector2 hitPosition)
    {
        GameObject ship = ShipOnPosition(hitPosition);

        if (ship != null)
        {
            ship.transform.position = hitPosition;            
            ship.SetActive(true);
            return true;
        }

        return false;
    }


    public Vector2 MakeMove()
    {
        Vector2 pos = RandomPositionPlayer();
        if (guessed.Contains(pos))
        {
            return MakeMove();
        }

        guessed.Add(pos);
        return pos;
    }


    private Vector2 RandomPositionPlayer()
    {
        return new Vector2(Random.Range(1, 9), Random.Range(1, 9));
    }

    private Vector2 RandomPosition()
    {
        return new Vector2(Random.Range(-8, 0), Random.Range(1, 9));
    }


    private GameObject ShipOnPosition(Vector2 pos)
    {
        foreach (GameObject ship in ships)
        {
            if (ship.transform.position == (Vector3)pos)
            {
                return ship;
            }
        }
        return null;
    }

    public bool AllShipsFound()
    {
        foreach (GameObject ship in ships)
        {
            if (!ship.activeSelf)
            {
                return false;
            }
        }
        return true;
    }
}

