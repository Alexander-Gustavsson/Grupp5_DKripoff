using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class Languages : MonoBehaviour
{
    public static event Action<string> NewLanguage;

    private static string[] languages =
    {
        "sv",
        "en"
    };

    public static string language = "en";

    public static Dictionary<(string, string), string> Texts = new Dictionary<(string, string), string>
    {
        { ("sv", "To Battle!"), "Till Strid!" },
        { ("sv", "Guides: On"), "Vägledning: På" },
        { ("sv", "Guides: Off"), "Vägledning: Av" },
        { ("sv", "Difficulty: Easy"), "Svårighet: Enkelt" },
        { ("sv", "Difficulty: Medium"), "Svårighet: Medelt" },
        { ("sv", "Difficulty: Hard"), "Svårighet: Svårt" },
        { ("sv", "Language:"), "Språk: Svenska" },
        { ("sv", "Done"), "Redo" },
        { ("sv", "Menu"), "Ge Upp" },
        { ("sv", "PlacementG"), "Dra dina skepp från nedre högra sidan till dina vatten.\r\n\r\nDe får inte placeras bredvid varandra!\r\n\r\nArrr!" },
        { ("sv", "RotationG"), "Klicka på skeppen för att rotera dem.\r\n\r\nKom ihåg – skepp får aldrig ligga intill varandra.\r\n\r\nKors i taket!" },
        { ("sv", "ShootingG"), "Tryck på motståndarens bräde för att avfyra dina kanoner!\r\n\r\nFörsök hitta alla deras skepp – de har samma form som dina!" },
        { ("sv", "DoneG"), "Klicka på färdig när du är redo för striden!" },
        { ("sv", "Win"), "Du vann! \r\nDina rangordningspoäng steg:" },
        { ("sv", "Loss"), "Du förlorade! \r\nDina rangordningspoäng sjönk:" },
        { ("sv", "Player's Turn"), "DIN TUR" },
        { ("sv", "Enemy Turn"), "FIENDENS TUR" },
        { ("sv", "Leave"), "Fortsätt" },
        { ("sv", "Stay"), "Ge Upp" },
        { ("sv", "Rank"), "Din Rangordning:" },
        { ("sv", "Lieutenant"), "Löjtnant" },
        { ("sv", "Sergeant-Major"), "Sergeantmajor" },
        { ("sv", "Navy Colonel"), "Marinens Överste" },
        { ("sv", "Music Volume"), "Musikens Volym" },
        { ("sv", "Sound Effects Volume"), "Ljudeffekter" },
        { ("sv", "Vibrations"), "Vibrationer" },
        { ("sv", "Settings"), "Inställningar" },
        { ("sv", "Close"), "Tillbaka" },
        { ("sv", "Toggle"), "Växla" },


        { ("en", "To Battle!"), "To Battle!" },
        { ("en", "Guides: On"), "Guides: On" },
        { ("en", "Guides: Off"), "Guides: Off" },
        { ("en", "Difficulty: Easy"), "Difficulty: Easy" },
        { ("en", "Difficulty: Medium"), "Difficulty: Medium" },
        { ("en", "Difficulty: Hard"), "Difficulty: Hard" },
        { ("en", "Done"), "Done" },
        { ("en", "Menu"), "Surrender" },
        { ("en", "Language:"), "Language: English" },
        { ("en", "PlacementG"), "Drag your ships from the bottom right to your waters.\r\n\r\nThey cannot be placed next to one another! \r\n\r\nArrg!" },
        { ("en", "RotationG"), "Click the ships to rotate them.\r\n\r\nRemember - ships can never be adjacent to one another." },
        { ("en", "ShootingG"), "Press the opponent's board to fire your cannons!\r\n\r\nTry to find all their ships - they have the same shape as yours!" },
        { ("en", "DoneG"), "One you're finished setting up, hit done!" },
        { ("en", "Win"), "You won! \r\nThis earned you rank points:" },
        { ("en", "Loss"), "You lost! \r\nYou lost rank points:" },
        { ("en", "PLAYER TURN"), "PLAYER TURN" },
        { ("en", "ENEMY TURN"), "ENEMY TURN" },
        { ("en", "Leave"), "Leave" },
        { ("en", "Stay"), "Stay" },
        { ("en", "Rank"), "Your Army Rank:" },
        { ("en", "Lieutenant"), "Lieutenant" },
        { ("en", "Sergeant-Major"), "Sergeant-Major" },
        { ("en", "Navy Colonel"), "Navy Colonel" },
        { ("en", "Music Volume"), "Music Volume" },
        { ("en", "Sound Effects Volume"), "Sound Effects Volume" },
        { ("en", "Vibrations"), "Haptic Feedback" },
        { ("en", "Settings"), "Settings" },
        { ("en", "Close"), "Close" },
        { ("en", "Toggle"), "Toggle" },

    };

    public void CycleLanguages()
    {
        try
        {
            language = languages[Array.IndexOf(languages, language) + 1];
        }
        catch
        {
            language = languages[0];
        }

        NewLanguage.Invoke(language);
    }
}
