using System.Collections.Generic;
// using UnityEditor;
using UnityEngine;
// using static UnityEditor.PlayerSettings;


public class AI : MonoBehaviour
{
    [SerializeField] private GameObject[] ships;

    List<Vector2> guessed = new List<Vector2>();
    List<GameObject> activeShips = new List<GameObject>();

    private void Awake()
    {
        activeShips.AddRange(ships);
    }


    // Kan l�gga till om skeppen ska kunna roteras senare
    public void PlaceShips()
    {
        foreach (GameObject ship in activeShips)
        {
            Vector2 pos = RandomPosition();
            bool placed = false;
            while (!placed)
            {
                if (ShipOnPosition(pos) == null)
                {
                    ship.transform.position = pos;
                    placed = true;
                }
            }

            ship.GetComponent<ShipShape>().ShipPlaced();
            ship.GetComponent<SpriteRenderer>().enabled = false;
        }

    }

    public GameObject TakeHit(Vector2 hitPos)
    {
        foreach (GameObject ship in activeShips)
        {

            var shape = ship.GetComponent<ShipShape>();

            if (ship.GetComponent<ShipShape>().IsShipHit(hitPos))
            {

                return ship;
                
            }
        }
        return null;
    }

    public bool IsShipGone(GameObject ship, Vector2 hitPos) {
        if (ship.GetComponent<ShipShape>().IsShipGone())
        {
            ship.GetComponent<SpriteRenderer>().enabled = true;
            activeShips.Remove(ship);
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
        foreach (GameObject ship in activeShips)
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
        return activeShips.Count == 0 ? true : false;
    }
}

