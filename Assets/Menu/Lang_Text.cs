using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Lang_Text : MonoBehaviour
{
    [SerializeField] public string textID;
    private TextMeshProUGUI text;

    private void OnEnable()
    {
        text = GetComponent<TextMeshProUGUI>();
        Languages.NewLanguage += ChangeText;
        ChangeText(Languages.language);
    }

    public void ChangeText(string lang)
    {
        print(textID);
        Languages.Texts.TryGetValue((lang, textID), out string value);
        text.text = value;
    }
}