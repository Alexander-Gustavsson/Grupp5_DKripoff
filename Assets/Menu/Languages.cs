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
        { ("sv", "To Battle!"), "Till Krig!" },
        { ("sv", "Guides: On"), "Vägledning: På" },
        { ("sv", "Guides: Off"), "Vägledning: Av" },
        { ("sv", "Difficulty: Easy"), "Svårighet: Enkelt" },
        { ("sv", "Language:"), "Språk: Svenska" },
        { ("sv", ""), "" },

        { ("en", "To Battle!"), "To Battle!" },
        { ("en", "Guides: On"), "Guides: On" },
        { ("en", "Guides: Off"), "Guides: Off" },
        { ("en", "Difficulty: Easy"), "Difficulty: Easy" },

        { ("en", "Language:"), "Language: English" }

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
