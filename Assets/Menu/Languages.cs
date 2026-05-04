using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class Languages : MonoBehaviour
{
    private static string[] languages =
    {
        "sv",
        "en"
    };

    private static string language = "en";

    public static Dictionary<(string, string), string> Text = new Dictionary<(string, string), string>
    {
        { ("sv", "To Battle!"), "Till Krig!" },
        { ("sv", "Guides:"), "Vägledning:" },
        { ("sv", "On"), "Av" },
        { ("sv", "Off"), "På" },
        { ("sv", "Language"), "Språk: Svenska" },
        { ("sv", ""), "" }


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
        
        
    }
}
