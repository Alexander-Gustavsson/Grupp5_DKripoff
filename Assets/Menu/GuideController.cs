using System;
using System.Collections.Generic;
using UnityEngine;

public class GuideController : MonoBehaviour
{
    public static bool guidesOn = false;

    public static event Action<GuideName> OnGuideActive;

    public enum GuideName
    {
        PLACE_SHIPS,
        SHOOT_SHIPS,
        ROTATE_SHIPS
    }
    public static List<GuideName> activeGuides = new List<GuideName>();
    // The dictionary allows for retrieving data about each specific guide.
    // Currently, only a bool is stored (denoting whether the guide is active or not).
    // Since it is a static variable, we can track whether players have interacted with a guide before in the session.

    private void Start()
    {
        if (guidesOn)
        {
            AddGuides();
        }
    }

    public static void TriggerGuide(GuideName guide)
    {
        foreach (var item in activeGuides)
        {
            print(item);
        }
        if (activeGuides.Contains(guide))
        {

            OnGuideActive.Invoke(guide);
        }
    }

    public void AddGuides()
    {   // Add a guide to the array of each type. Uses Enum instead of String to add new values easier.
        foreach (GuideName guide in Enum.GetValues(typeof(GuideName))) {
            activeGuides.Add(guide);
        }
    }

    public void ToggleGuides()
    {
        if (activeGuides.Count > 0)
        {
            activeGuides.Clear();
            guidesOn = false;
        }

        else AddGuides();
    }
}