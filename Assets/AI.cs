using System.Collections.Generic;
// using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;
// using static UnityEditor.PlayerSettings;


public class AI : MonoBehaviour
{
    [SerializeField] private GameObject[] ships;

    List<Vector2> guessed = new List<Vector2>();
    List<GameObject> activeShips = new List<GameObject>();

    //lista för ai:n att fortsätta jaga skepp
    List<Vector2> nextAIMove = new List<Vector2>();
    private Vector2 continueDir;
    public bool isAttacking;
    public bool foundDir;
    public int counter = 0;
    public Vector2 firstHit, secondHit;
    public bool switchDir;

    private Vector2 lastPos;

    private void Awake()
    {
        activeShips.AddRange(ships);
    }


    // Kan l�gga till om skeppen ska kunna roteras senare
    public void PlaceShips()
    {
        foreach (GameObject ship in activeShips)
        {
            bool placed = false;
            while (!placed)
            {
                ship.transform.position = RandomPosition();
                int rotation = RandomShipRotation(ship);
                ship.GetComponent<ShipShape>().ShipPlaced();

                if (ValidPosition(ship))
                {
                    placed = true;
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
        Vector2 tryPos = DiagonalPos();

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

    public Vector2 HardAIMakeMove()
    {
        Vector2 tryPos = DiagonalPos();
        Vector2 pos = Vector2.zero;

        //ta första i listan sen fortsätt
        if (nextAIMove.Count > 0)
        {
            pos = nextAIMove[0];
            nextAIMove.RemoveAt(0); //removeat tar bort från listan
        }
        else if (counter == 2 && !foundDir)
        {
            FindDirection(firstHit, secondHit);
            foundDir = true;
            pos = secondHit + continueDir;
        }
        else if (foundDir && isAttacking)
        {
            if (switchDir == true || pos.x < 1 || pos.x > 8 || pos.y < 1 || pos.y > 8)
            {
                continueDir *= -1;
                switchDir = false;
                nextAIMove.Add(firstHit + continueDir);
            }
            else
            {
                nextAIMove.Add(lastPos + continueDir);
            }

            if (nextAIMove.Count > 0)
            {
                pos = nextAIMove[0];
                nextAIMove.RemoveAt(0);
            }
        }


        //ny ai
        //ny logik för svår ai, först lägger ut random sen fortsätter diagonalt tills utanför grid, ny random

        else if (lastPos.x < 0.5f || lastPos.y < 0.5f)
        {
            pos = RandomPositionPlayer();
        }
        else if (tryPos != Vector2.zero)
        {
            pos = tryPos;
        }
        else
        {
            pos = RandomPositionPlayer();
        }


        lastPos = pos;
        //ny ai

        while (guessed.Contains(pos))
        {
            pos = RandomPositionPlayer();
        }

        guessed.Add(pos);
        return pos;
    }

    private Vector2 DiagonalPos()
    {
        List<Vector2> positions = new List<Vector2>();
        positions.Add(lastPos + new Vector2(1, 1));
        positions.Add(lastPos + new Vector2(-1, 1));
        positions.Add(lastPos + new Vector2(1, -1));
        positions.Add(lastPos + new Vector2(-1, -1));

        positions = RandomizeList(positions);

        foreach (Vector2 tryPos in positions)
        {
            if (guessed.Contains(tryPos))
            {
                continue;
            }
            if (tryPos.x <= 8.5f && tryPos.y <= 8.5f && tryPos.x >= 0.5f && tryPos.y >= 0.5f)
            {
                return tryPos;
            }
        }
        return Vector2.zero;
    }

    private List<Vector2> RandomizeList(List<Vector2> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Vector2 temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
        return list;
    }


    //metod för att samla på nya attacker
    public void AddNextTargets(Vector2 hitPos)
    {

        List<Vector2> nextDirection = new List<Vector2>(); //kordinater nära skeppet
        nextDirection.Add(Vector2.up);
        nextDirection.Add(Vector2.down);
        nextDirection.Add(Vector2.right);
        nextDirection.Add(Vector2.left);

        nextDirection = RandomizeList(nextDirection);

        for (int i = 0; i < nextDirection.Count; i++)
        {
            Vector2 nextTarget = hitPos + nextDirection[i];

            if (nextTarget.x >= 1 && nextTarget.x <= 8 && nextTarget.y >= 1 && nextTarget.y <= 8)
            {
                if (!guessed.Contains(nextTarget) && !nextAIMove.Contains(nextTarget))//kollar så inte skjuten eller inte är dubbletter
                {
                    nextAIMove.Add(nextTarget);
                }
            }
        }
    }



    public void FindDirection(Vector2 firstHit, Vector2 secondHit)
    {
        continueDir = firstHit + Vector2.down == secondHit ? Vector2.down
            : firstHit + Vector2.up == secondHit ? Vector2.up
            : firstHit + Vector2.right == secondHit ? Vector2.right
            : Vector2.left;
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
                List<Vector2> temp = checkShip.GetComponent<ShipShape>().GetShipPositions();
                if (temp.Contains(pos) 
                    || pos.x < -8 || pos.x > -1 || pos.y < 1 || pos.y > 8
                    || temp.Contains(pos + Vector2.down)
                    || temp.Contains(pos + Vector2.up)
                    || temp.Contains(pos + Vector2.left)
                    || temp.Contains(pos + Vector2.right))
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

