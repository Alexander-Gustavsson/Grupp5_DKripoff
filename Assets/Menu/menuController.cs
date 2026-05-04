using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuController : MonoBehaviour
{
    [SerializeField] private GameObject rankPanel;



    public void LoadGame()
    {
        GameObject.Find("AudioManager").GetComponent<Music>().SmoothSound(0.3f, 2f);

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


}