using System.Collections.Generic;
// using UnityEditor;
using UnityEngine;
// using static UnityEditor.PlayerSettings;


public class AI : MonoBehaviour
{
    [SerializeField] private GameObject[] ships;

    List<Vector2> guessed = new List<Vector2>();
    List<GameObject> activeShips = new List<GameObject>();

    //lista för ai:n att fortsätta jaga skepp
    List<Vector2> nextAIMove = new List<Vector2>();
    private Vector2 continueDir;

    private void Awake()
    {
        activeShips.AddRange(ships);
    }


    // Kan l�gga till om skeppen ska kunna roteras senare
    public void PlaceShips()
    {
        foreach (GameObject ship in activeShips)
        {
            while (true)
            {
                ship.transform.position = RandomPosition();
                int rotation = RandomShipRotation(ship);
                ship.GetComponent<ShipShape>().ShipPlaced();

                if (ValidPosition(ship))
                {
                    break;
                }
                ship.transform.Rotate(0, 0, -rotation);
            }

            //ship.GetComponent<ShipShape>().ShipPlaced();
            ship.SetActive(false);
        }

    }

    public GameObject TakeHit(Vector2 hitPos)
    {
        foreach (GameObject ship in activeShips)
        {
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
            ship.SetActive(true);
            activeShips.Remove(ship);
            return true;
        }
        return false;
    }

    public Vector2 MakeMove()
    {

        //ta första i listan sen fortsätt
        Vector2 pos;
        if (nextAIMove.Count > 0)
        {
            pos = nextAIMove[0];
            nextAIMove.RemoveAt(0); //removeat tar bort från listan
        }
        else
        {
             pos = RandomPositionPlayer();// ANNARS skjut slump
        }

        if (guessed.Contains(pos))
        {
            return MakeMove();
        }

        guessed.Add(pos);
        return pos;
    }


    //metod för att samla på nya attacker
    public void AddNextTargets(Vector2 hitPos)
    {
        Vector2[] nextDirection =
        {
            Vector2.up, Vector2.down, Vector2.left, Vector2.right //kordinaterna nära skeppet
        };

        for (int i = 0; i < nextDirection.Length; i++)
        {
               
            Vector2 dir = nextDirection[Random.Range(0, nextDirection.Length - 1)];
            Vector2 nextTarget = hitPos + dir;

            if (nextTarget.x >= 1 && nextTarget.x <= 8 && nextTarget.y >= 1 && nextTarget.y <= 8)
            {
                if (!guessed.Contains(nextTarget) && !nextAIMove.Contains(nextTarget))//kollar så inte skjuten eller inte är dubbletter
                {
                    nextAIMove.Add(nextTarget);
                }
            }

        }
       
    }

    public void ClearTargets()
    {
        nextAIMove.Clear();
    }

    private Vector2 RandomPositionPlayer()
    {
        return new Vector2(Random.Range(1, 9), Random.Range(1, 9));
    }

    private Vector2 RandomPosition()
    {
        return new Vector2(Random.Range(-8, 0), Random.Range(1, 9));
    }


    private bool ValidPosition(GameObject ship)
    {
        foreach (GameObject checkShip in activeShips)
        {
            if (checkShip == ship)
            {
                continue;
            }

            foreach (Vector2 pos in ship.GetComponent<ShipShape>().GetShipPositions())
            {
                if (checkShip.GetComponent<ShipShape>().GetShipPositions().Contains(pos) || pos.x < -8 || pos.x > -1 || pos.y < 1 || pos.y > 8)
                {
                    return false;
                }
            }
        }
        return true;
    }

    private int RandomShipRotation(GameObject ship)
    {
        int rotation = Random.Range(0, 4) * 90;
        ship.transform.Rotate(0, 0, rotation);
        return rotation;
    }

    public bool AllShipsFound()
    {
        return activeShips.Count == 0 ? true : false;
    }
}

