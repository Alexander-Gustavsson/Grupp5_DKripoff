using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuController : MonoBehaviour //Can't change this to abide by naming conventions as it causes my computer to lose the file.
{
    [SerializeField] private GameObject rankPanel;
    [SerializeField] private GameObject settingsPanel;



    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenSettingsPanel()
    {
        settingsPanel.SetActive(true);
    }
    public void CloseSettingsPanel()
    {
        settingsPanel.SetActive(false);
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