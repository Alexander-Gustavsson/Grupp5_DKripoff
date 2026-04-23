using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Android;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

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
}