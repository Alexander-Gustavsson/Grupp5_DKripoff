using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject rankPanel;



    public void LoadGame()
    {
        GameObject.Find("AudioManager").GetComponent<MusicPlayer>().PlayPreparation();

        SceneManager.LoadScene(1);
    }

    public void OpenRankPanel()
    {
        rankPanel.SetActive(true);
    }

    public void CloseRankPanel()
    {
        rankPanel.SetActive(false);
    }
    public void StartEasyAI()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().StartEasyAI();
        LoadGame();
    }

    public void StartMediumAI()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().StartMediumAI();
        LoadGame();
    }

    public void StartHardAI()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().StartHardAI();
        LoadGame();
    }

}