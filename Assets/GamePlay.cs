using JetBrains.Annotations;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class GamePlay : MonoBehaviour
{
    [SerializeField] private AI AI;

    [SerializeField] private GameObject[] ships;
    [SerializeField] private GameObject missSprite1;
    [SerializeField] private GameObject missSprite2;
    [SerializeField] private GameObject hitShipSprite;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject menuButton;
    [SerializeField] private GameObject returnToMenuPanel;
    //[SerializeField] private GameManager gameManager;


    //Animations: 
    [SerializeField] private TileHighlight tileHighlight;


    List<Vector2> missedPos = new List<Vector2>();
    List<Vector2> guessedPos = new List<Vector2>();
    List<GameObject> activeShips = new List<GameObject>();
    private InputClick clickScript;
    [SerializeField] private ShotFeedbackManager shotFeedback;
    [SerializeField] private TurnIndicatorUI turnIndicatorUI;

    List<GameObject> placedShips = new List<GameObject>();


    void Start()
    {
        Invoke("TriggerPlacementGuide", 0f);
        clickScript = GetComponent<InputClick>();
        activeShips.AddRange(ships);
        placedShips.AddRange(activeShips);
        PlaceShips();
    }

    private void TriggerPlacementGuide()
    {
        GuideController.TriggerGuide(GuideController.GuideName.PLACE_SHIPS);
    }

    public void AIGridPressed(Vector2 pressPos)
    {
        Vector2 gridPos = new Vector2(Mathf.Round(pressPos.x), Mathf.Round(pressPos.y));

        tileHighlight?.ShowHighlight(gridPos);
        
        turnIndicatorUI?.ShowPlayerTurn();

        tileHighlight?.ShowHighlight(gridPos);
        

        // Handle reclick
        if (guessedPos.Contains(gridPos))
        {
            return;
        }
        //anim:
        if (shotFeedback != null) shotFeedback.PlayFire(gridPos);
        GameObject ship = AI.TakeHit(gridPos);
       
        if (ship != null)
        {
            SpawnHitShipSprite(gridPos);
            if (AI.IsShipGone(ship, gridPos))
            {
                //anim:
                ShipSunkVisual sunkVisual = ship.GetComponent<ShipSunkVisual>();
                if (sunkVisual != null)
                {
                    sunkVisual.MarkAsSunk();
                }

                if (shotFeedback != null) shotFeedback.PlaySink(gridPos);

                if (AI.AllShipsFound())
                {
                    Win();
                }
            }
            else
            {
                if (shotFeedback != null) shotFeedback.PlayHit(gridPos);
            }

            return;
        }
        SpawnMissSprite(gridPos);
        //aanimation:
        if (shotFeedback != null) shotFeedback.PlayMiss(gridPos);
        clickScript.canClick = false;
        if (turnIndicatorUI != null)
        {
            turnIndicatorUI.ShowEnemyTurn();
        }

        Invoke("MakeAIMove", 0.8f);


        if (AI.AllShipsFound())
        {
            Win();
            return;
        }

    
    }

    private void MakeAIMove()

    {
        if (turnIndicatorUI != null)
        {
            turnIndicatorUI.ShowEnemyTurn();
        }
        Vector2 hitPos = AI.MakeMove();

        if (shotFeedback != null) shotFeedback.PlayFire(hitPos);
        foreach (GameObject ship in activeShips)
        {
            if (ship.GetComponent<ShipShape>().IsShipHit(hitPos))
            {
                //ain l�gger till rutorna n�ra skeppet om det finns (f�rsta prioritet)
                AI.isAttacking = true;
                AI.counter += 1;
                if (AI.counter == 1)
                {
                    AI.AddNextTargets(hitPos);
                }

                if (AI.counter == 1)
                {
                    AI.firstHit = hitPos;
                }
                else if (AI.counter == 2)
                {
                    AI.ClearTargets();
                    AI.secondHit = hitPos;
                }

                if (ship.GetComponent<ShipShape>().IsShipGone())
                {
                    //code here if entire ship is gone
                    activeShips.Remove(ship);
                    AI.isAttacking = false;
                    AI.counter = 0;
                    AI.foundDir = false;
                    AI.ClearTargets();

                    //anim:
                    ShipSunkVisual sunkVisual = ship.GetComponent<ShipSunkVisual>();
                    if (sunkVisual != null)
                    {
                        sunkVisual.MarkAsSunk();
                    }

                    SpawnHitShipSprite(hitPos);
                    if (shotFeedback != null) shotFeedback.PlaySink(hitPos);
                }
                else
                {
                    SpawnHitShipSprite(hitPos);
                    if (shotFeedback != null) shotFeedback.PlayHit(hitPos);
                }

                if (AllPlayerShipFound())
                {
                    Lose();
                    return;
                }

                Invoke("MakeAIMove", 0.5f);
                return;
            }
        }

        GameObject missSprite = missSprite1;
        if ((hitPos.x + hitPos.y) % 2 == 0)
        {
            missSprite = missSprite2;
        }

        Instantiate(missSprite, hitPos, Quaternion.identity);
        if (shotFeedback != null) shotFeedback.PlayMiss(hitPos);

        if (AI.isAttacking && AI.foundDir)
        {
            AI.switchDir = true;
        }

        MakePlayerMove();
    }

    private void MakePlayerMove()

    {
        if (turnIndicatorUI != null)
        {
            turnIndicatorUI.ShowPlayerTurn();
        }

        clickScript.canClick = true;
        
    }

    private void PlaceShips()
    {
        clickScript.canDrag = true;
        AI.PlaceShips();
    }

    public void CheckAllShipsPlaced()
    {
        foreach (GameObject ship in activeShips)
        {
            if (!ship.GetComponent<DragDrop>().isValid())
            {
                return;
            }
        }
        startButton.GetComponent<Button>().interactable = true;
    }

    // K�rs efter man har placerat ut alla skepp, m�ste kallas p� med ex en knapp
    public void StartGamePlay()
    {
        GuideController.TriggerGuide(GuideController.GuideName.SHOOT_SHIPS);
        GameObject.Find("Main Camera").GetComponent<CamControl>().EnterCombat();
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
        GameObject missSprite;

        if ((pos.x + pos.y) % 2 == 0)
        {
            missSprite = missSprite2;
        } else
        {
            missSprite = missSprite1;
        }

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

    public void Lose()
    {
        clickScript.canClick = false;

        GameManager GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        GM.PlayerLost();

        GameObject ls = GameObject.Find("Panel - Loss");
        ls.GetComponent<Image>().enabled = true;
        ls.transform.Find("Loss Text").gameObject.SetActive(true);
        ls.transform.Find("Score Text").gameObject.SetActive(true);

        TextMeshProUGUI text = ls.transform.Find("Score Text").GetComponent<TextMeshProUGUI>();
        text.enabled = true;
        text.text += "~~ " + GM.decreaseRankPoints + " ~~";

    }

    // Kan l�gga till saker h�r om spelaren vinner
    public void Win()
    {
        clickScript.canClick = false;

        GameManager GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        GM.PlayerWon();

        GameObject ws = GameObject.Find("Panel - Win");
        ws.GetComponent<Image>().enabled = true;
        ws.transform.Find("Victory Text").gameObject.SetActive(true);
        ws.transform.Find("Score Text").gameObject.SetActive(true);

        TextMeshProUGUI text = ws.transform.Find("Victory Score").GetComponent<TextMeshProUGUI>();
        text.enabled = true;
        text.text += "~~ " + GM.increaseRankPoints + " ~~";
    }

    public void UpenReturnDialogue()
    {
        returnToMenuPanel.SetActive(true);
    }
    public void CloseReturnDialogue()
    {
        returnToMenuPanel.SetActive(false);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
