using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GamePlay : MonoBehaviour
{
    [SerializeField] private AI AI;

    [SerializeField] private GameObject[] ships;
    [SerializeField] private GameObject missSprite;
    [SerializeField] private GameObject startButton;

    List<Vector2> missedPos = new List<Vector2>();

    private InputClick clickScript;


    void Start()
    {
        clickScript = GetComponent<InputClick>();
        PlaceShips();
    }

    public void AIGridPressed(Vector2 pressPos)
    {
        Vector2 gridPos = new Vector2(Mathf.Round(pressPos.x), Mathf.Round(pressPos.y));

        if (!AI.TakeHit(gridPos))
        {
            SpawnMissSprite(gridPos);
            clickScript.canClick = false;

            Invoke("MakeAIMove", 0.5f);
        }

        if (AI.AllShipsFound())
        {
            Win();
            return;
        }
    }

    private void MakeAIMove()
    {
        Vector2 hitPos = AI.MakeMove();

        foreach (GameObject ship in ships)
        {
            if (ship.transform.position == (Vector3)hitPos)
            {
                ship.SetActive(false);
                if (AllPlayerShipFound())
                {
                    Lose();
                    return;
                }
                MakeAIMove();
                return;
            }
        }

        SpawnMissSprite(hitPos);
        MakePlayerMove();
    }

    private void MakePlayerMove()
    {
        clickScript.canClick = true;
    }

    private void PlaceShips()
    {
        clickScript.canDrag = true;
        AI.PlaceShips();
    }

    // Körs efter man har placerat ut alla skepp, måste kallas på med ex en knapp
    public void StartGamePlay()
    {
        startButton.SetActive(false);
        clickScript.canDrag = false;
        MakePlayerMove();
    }

    private void SpawnMissSprite(Vector2 pos)
    {
        if (missedPos.Contains(pos))
        {
            return;
        }
        missedPos.Add(pos);
        Instantiate(missSprite, pos, Quaternion.identity);
    }

    private bool AllPlayerShipFound()
    {
        foreach (GameObject ship in ships)
        {
            if (ship.activeSelf)
            {
                return false;
            }
        }
        return true;
    }

    // Kan lägga till saker här om spelaren förlorar
    public void Lose()
    {
        //Tillbaka till meny
    }

    // Kan lägga till saker här om spelaren vinner
    public void Win()
    {
        //Tillbaka till meny
    }
}
