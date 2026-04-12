using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Android;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    [SerializeField] private int rankPoints = 0;
    [SerializeField] private TMP_Text textRank, textRankPoints;
    [SerializeField] private GameObject imagePanelRank1, imagePanelRank2, imagePanelRank3, imageRank1, imageRank2, imageRank3;
    [SerializeField] private Slider sliderRank;

    public int rank;
    private int prevRank;
    private int increaseRankPoints;
    private int decreaseRankPoints;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateRank();
        prevRank = rank;
    }

    public void PlayerWon()
    {
        rankPoints += increaseRankPoints;
        UpdateRank();
        // add coins 
    }

    public void PlayerLost()
    {
        rankPoints -= decreaseRankPoints;
        UpdateRank();
    }

    private void UpdateRank()
    {
        rank = rankPoints < 300 ? 1 : rankPoints < 1000 ? 2 : 3;

        if (rank != prevRank)
        {
            SetRankSettings();
        }

        sliderRank.value = rankPoints;
    }

    private void SetRankSettings()
    {
        switch (rank)
        {
            case 1:
                textRank.text = "Silver";
                textRankPoints.text = rankPoints + "/300";

                imagePanelRank1.SetActive(true);
                imagePanelRank2.SetActive(false);
                imageRank1.SetActive(true);
                imageRank2.SetActive(false);

                sliderRank.maxValue = 300;
                sliderRank.minValue = 0;

                increaseRankPoints = 50;
                decreaseRankPoints = 10;

                prevRank = 1;

                break;

            case 2:
                textRank.text = "Gold";
                textRankPoints.text = rankPoints + "/1000";

                imagePanelRank1.SetActive(false);
                imagePanelRank2.SetActive(true);
                imagePanelRank3.SetActive(false);
                imageRank1.SetActive(false);
                imageRank2.SetActive(true);
                imageRank3.SetActive(false);

                sliderRank.maxValue = 1000;
                sliderRank.minValue = 300;
                sliderRank.gameObject.SetActive(true);

                increaseRankPoints = 75;
                decreaseRankPoints = 20;

                prevRank = rank;

                break;

            case 3:
                textRank.text = "Platinum";
                textRankPoints.text = "" + rankPoints;

                imagePanelRank2.SetActive(false);
                imagePanelRank3.SetActive(true);
                imageRank2.SetActive(false);
                imageRank3.SetActive(true);

                sliderRank.gameObject.SetActive(false);

                increaseRankPoints = 100;
                decreaseRankPoints = 30;

                prevRank = 3;

                break;
        }
    }

    private void SpawnAIBasedOnRank()
    {
        //spawn right AI based on rank

        //write here or have a switch case in AI script with rank value
        //spawn automatic or have buttons for each difficulty?
    }
}
