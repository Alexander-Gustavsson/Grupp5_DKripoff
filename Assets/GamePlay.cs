using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GamePlay : MonoBehaviour
{
    [SerializeField] private AI AI;

    [SerializeField] private GameObject[] ships;
    [SerializeField] private GameObject missSprite;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject menuButton;


    //Animations:
    [SerializeField] private TileHighlight tileHighlight;
    List<Vector2> missedPos = new List<Vector2>();

    List<Vector2> guessedPos = new List<Vector2>();
    List<GameObject> activeShips = new List<GameObject>();

    private InputClick clickScript;


    void Start()
    {
        clickScript = GetComponent<InputClick>();
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

                //ain l�gger till rutorna n�ra skeppet om det finns (f�rsta prioritet)
                AI.AddNextTargets(hitPos);

                if (ship.GetComponent<ShipShape>().IsShipGone())
                {
                    //code here if entire ship is hit
                    activeShips.Remove(ship);
                }
                SpawnHitShipSprite(hitPos);

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

    // K�rs efter man har placerat ut alla skepp, m�ste kallas p� med ex en knapp
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

    // Kan l�gga till saker h�r om spelaren f�rlorar
    public void Lose()
    {
        //Tillbaka till meny
    }

    // Kan l�gga till saker h�r om spelaren vinner
    public void Win()
    {
        //Tillbaka till meny
    }

    public void ReturnToMainMenu()
    {
        GameObject.Find("AudioManager").GetComponent<Music>().SmoothSound(0.6f, 2f);

        SceneManager.LoadScene(0);
    }
}
