using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Android;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public int rankPoints = 700;
    public int rank;
    public int increaseRankPoints;
    public int decreaseRankPoints;
    public int spawnAI;


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
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
    }

    public void StartEasyAI()
    {
        spawnAI = 1;
        increaseRankPoints = 70;
        decreaseRankPoints = 5;
        LoadGame();
    }

    public void StartMediumAI()
    {
        spawnAI = 2;
        increaseRankPoints = 75;
        decreaseRankPoints = 50;
        LoadGame();
    }

    public void StartHardAI()
    {
        spawnAI = 3;
        increaseRankPoints = 100;
        decreaseRankPoints = 100;
        LoadGame();
    }

    public void LoadGame()
    {
        GameObject.Find("AudioManager").GetComponent<Music>().SmoothSound(0.3f, 2f);

        SceneManager.LoadScene(1);
    }
}