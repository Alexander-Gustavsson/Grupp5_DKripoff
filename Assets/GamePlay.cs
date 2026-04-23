using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlay : MonoBehaviour
{
    [SerializeField] private AI AI;

    [SerializeField] private GameObject[] ships;
    [SerializeField] private GameObject missSprite;
    [SerializeField] private GameObject hitShipSprite;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject menuButton;
    //[SerializeField] private GameManager gameManager;


    //Animations:
    [SerializeField] private TileHighlight tileHighlight;
    List<Vector2> missedPos = new List<Vector2>();
    List<Vector2> guessedPos = new List<Vector2>();
    List<GameObject> activeShips = new List<GameObject>();
    private InputClick clickScript;

    List<GameObject> placedShips = new List<GameObject>();


    void Start()
    {
        clickScript = GetComponent<InputClick>();
        activeShips.AddRange(ships);
        placedShips.AddRange(activeShips);
        PlaceShips();
    }

    public void AIGridPressed(Vector2 pressPos)
    {
        Vector2 gridPos = new Vector2(Mathf.Round(pressPos.x), Mathf.Round(pressPos.y));
        //Animations:
        if (tileHighlight != null)
        {
            tileHighlight.ShowHighlight(gridPos);
        }

        // Handle reclick
        if (guessedPos.Contains(gridPos))
        {
            return;
        }
        GameObject ship = AI.TakeHit(gridPos);

        if (ship != null)
        {
            SpawnHitShipSprite(gridPos);
            if (AI.IsShipGone(ship, gridPos))
            {
                if (AI.AllShipsFound())
                {
                    Win();
                }
            }
            return;
        }
        SpawnMissSprite(gridPos);
        clickScript.canClick = false;

        Invoke("MakeAIMove", 0.5f);


        if (AI.AllShipsFound())
        {
            Win();
            return;
        }
    }

    private void MakeAIMove()
    {
        Vector2 hitPos = AI.MakeMove();

        foreach (GameObject ship in activeShips)
        {
            if (ship.GetComponent<ShipShape>().IsShipHit(hitPos))
            {
                //ain lägger till rutorna nära skeppet om det finns (första prioritet)
                AI.AddNextTargets(hitPos);

                if (ship.GetComponent<ShipShape>().IsShipGone())
                {
                    //code here if entire ship is hit
                    activeShips.Remove(ship);
                    AI.ClearTargets();
                }
                SpawnHitShipSprite(hitPos);

                if (AllPlayerShipFound())
                {
                    Lose();
                    return;
                }

                Invoke("MakeAIMove", 0.5f);
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
        foreach (GameObject ship in activeShips)
        {
            ship.GetComponent<ShipShape>().ShipPlaced();
        }
        MakePlayerMove();
    }

    private void SpawnMissSprite(Vector2 pos)
    {
        if (guessedPos.Contains(pos))
        {
            return;
        }
        guessedPos.Add(pos);
        Instantiate(missSprite, pos, Quaternion.identity);
    }

    private void SpawnHitShipSprite(Vector2 pos)
    {
        if (guessedPos.Contains(pos))
        {
            return;
        }
        guessedPos.Add(pos);
        Instantiate(hitShipSprite, pos, Quaternion.identity);
    }

    private bool AllPlayerShipFound()
    {
        return activeShips.Count == 0 ? true : false;
    }

    // Kan lägga till saker här om spelaren förlorar
    public void Lose()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().PlayerLost();
    }

    // Kan lägga till saker här om spelaren vinner
    public void Win()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().PlayerWon();
    }

    public void ReturnToMainMenu()
    {
        GameObject.Find("AudioManager").GetComponent<Music>().SmoothSound(0.6f, 2f);

        SceneManager.LoadScene(0);
    }
}
