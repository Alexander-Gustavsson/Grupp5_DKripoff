using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RankManager : MonoBehaviour
{
    [SerializeField] private TMP_Text textRank, textRankPoints;
    [SerializeField] private GameObject imagePanelRank1, imagePanelRank2, imagePanelRank3, imageRank1, imageRank2, imageRank3;
    [SerializeField] private Slider sliderRank;

    private GameManager gameManager;
    private int prevRank = 0;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        UpdateRank();
    }

    private void UpdateRank()
    {
        gameManager.rank = gameManager.rankPoints < 300 ? 1 : gameManager.rankPoints < 1000 ? 2 : 3;

        if (gameManager.rank != prevRank)
        {
            SetRankSettings();
        }

        sliderRank.value = gameManager.rankPoints;
    }

    private void SetRankSettings()
    {
        switch (gameManager.rank)
        {
            case 1:
                textRank.text = "Silver";
                textRankPoints.text = gameManager.rankPoints + "/300";

                imagePanelRank1.SetActive(true);
                imagePanelRank2.SetActive(false);
                imageRank1.SetActive(true);
                imageRank2.SetActive(false);

                sliderRank.maxValue = 300;
                sliderRank.minValue = 0;

                gameManager.increaseRankPoints = 50;
                gameManager.decreaseRankPoints = 10;

                prevRank = 1;

                break;

            case 2:
                textRank.text = "Gold";
                textRankPoints.text = gameManager.rankPoints + "/1000";

                imagePanelRank1.SetActive(false);
                imagePanelRank2.SetActive(true);
                imagePanelRank3.SetActive(false);
                imageRank1.SetActive(false);
                imageRank2.SetActive(true);
                imageRank3.SetActive(false);

                sliderRank.maxValue = 1000;
                sliderRank.minValue = 300;
                sliderRank.gameObject.SetActive(true);

                gameManager.increaseRankPoints = 75;
                gameManager.decreaseRankPoints = 20;

                prevRank = gameManager.rank;

                break;

            case 3:
                textRank.text = "Platinum";
                textRankPoints.text = "" + gameManager.rankPoints;

                imagePanelRank2.SetActive(false);
                imagePanelRank3.SetActive(true);
                imageRank2.SetActive(false);
                imageRank3.SetActive(true);

                sliderRank.gameObject.SetActive(false);

                gameManager.increaseRankPoints = 100;
                gameManager.decreaseRankPoints = 30;

                prevRank = 3;

                break;
        }
    }
}
