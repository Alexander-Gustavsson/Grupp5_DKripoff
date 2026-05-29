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
        gameManager.rank = gameManager.rankPoints < 200 ? 1 : gameManager.rankPoints < 500 ? 2 : 3;

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
                textRank.GetComponent<Lang_Text>().textID = "Sergeant-Major";
                textRankPoints.text = gameManager.rankPoints + "/200";

                imagePanelRank1.SetActive(true);
                imagePanelRank2.SetActive(false);
                imageRank1.SetActive(true);
                imageRank2.SetActive(false);

                sliderRank.maxValue = 300;
                sliderRank.minValue = 0;

                prevRank = 1;

                break;

            case 2:
                textRank.GetComponent<Lang_Text>().textID = "Lieutenant";
                textRankPoints.text = gameManager.rankPoints + "/500";

                imagePanelRank1.SetActive(false);
                imagePanelRank2.SetActive(true);
                imagePanelRank3.SetActive(false);
                imageRank1.SetActive(false);
                imageRank2.SetActive(true);
                imageRank3.SetActive(false);

                sliderRank.maxValue = 500;
                sliderRank.minValue = 200;
                sliderRank.gameObject.SetActive(true);

                prevRank = gameManager.rank;

                break;

            case 3:
                textRank.GetComponent<Lang_Text>().textID = "Navy Colonel";
                textRankPoints.text = "" + gameManager.rankPoints;

                imagePanelRank2.SetActive(false);
                imagePanelRank3.SetActive(true);
                imageRank2.SetActive(false);
                imageRank3.SetActive(true);

                sliderRank.gameObject.SetActive(false);

                prevRank = 3;

                break;
        }
    }
}
