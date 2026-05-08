using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Android;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public int spawnAI;
    public int rankPoints = 700;
    public int rank;
    public int increaseRankPoints;
    public int decreaseRankPoints;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayerWon()
    {
        rankPoints += increaseRankPoints;
    }

    public void PlayerLost()
    {
        rankPoints -= decreaseRankPoints;
        if (rankPoints < 0)
        {
            rankPoints = 0;
        }
    }

    public void StartEasyAI()
    {
        spawnAI = 1;
        increaseRankPoints = 70;
        decreaseRankPoints = 5;
    }

    public void StartMediumAI()
    {
        spawnAI = 2;
        increaseRankPoints = 75;
        decreaseRankPoints = 50;
    }

    public void StartHardAI()
    {
        spawnAI = 3;
        increaseRankPoints = 100;
        decreaseRankPoints = 100;
    }
}