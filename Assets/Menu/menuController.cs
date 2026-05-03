using UnityEngine;
using UnityEngine.SceneManagement;

public class menuController : MonoBehaviour
{
    [SerializeField] private GameObject rankPanel;

    //private void Start()
    //{
    //    AudioManager.PlaySound(SoundType.AMBIANCE, 0.6f);
    //}
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
    public void StartEasyAI()
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.spawnAI = 1;
        gameManager.increaseRankPoints = 70;
        gameManager.decreaseRankPoints = 5;
        LoadGame();
    }

    public void StartMediumAI()
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.spawnAI = 2;
        gameManager.increaseRankPoints = 75;
        gameManager.decreaseRankPoints = 50;
        LoadGame();
    }

    public void StartHardAI()
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.spawnAI = 3;
        gameManager.increaseRankPoints = 100;
        gameManager.decreaseRankPoints = 100;
        LoadGame();
    }
}