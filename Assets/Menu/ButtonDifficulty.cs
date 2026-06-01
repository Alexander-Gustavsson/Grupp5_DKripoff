using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ButtonDifficulty : MonoBehaviour
{
    [SerializeField] private Color highlightColor;
    [SerializeField] private Button playButton;

    private Image image;
    private GameManager gameManager;
    private Color baseColor;
    private Lang_Text langText;
    int current;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        langText = GetComponentInChildren<Lang_Text>();
        current = gameManager.rank;

        image = transform.Find("Image").GetComponent<Image>();
        baseColor = image.color;

        switch (gameManager.spawnAI)
        {
            case 1:
                Easy();
                break;
            case 2:
                Medium();
                break;
            case 3:
                Hard();
                break;
            default:
                Easy();
                break;
        }
    }

    public void ChangeDifficulty()
    {
        current++;
        if (current > 3)
        {
            current = 1;
        }
        
        switch (current)
        {
            case 1:
                Easy();
                break;
            case 2:
                Medium();
                break;
            case 3:
                Hard();
                break;
            default:
                break;
        }

        Highlight();
        langText.ChangeText(Languages.language);
    }

    private void Highlight()
    {
        if (gameManager.rank < current)
        {
            playButton.interactable = false;
            image.color = highlightColor;
        }
        else
        {
            playButton.interactable = true;
            image.color = baseColor;
        }
    }

    private void Easy()
    {
        current = 1;
        langText.textID = "Difficulty: Easy";
        gameManager.StartEasyAI();
    }

    private void Medium()
    {
        current = 2;
        langText.textID = "Difficulty: Medium";
        gameManager.StartMediumAI();
    }

    private void Hard()
    {
        current = 3;
        langText.textID = "Difficulty: Hard";
        gameManager.StartHardAI();
    }
}
