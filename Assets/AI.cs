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

    private void Awake()
    {
        activeShips.AddRange(ships);
    }


    // Kan l�gga till om skeppen ska kunna roteras senare
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

        foreach (Vector2 dir in nextDirection)
        {
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

