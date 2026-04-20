using System;
using System.Collections.Generic;
using UnityEngine;

public class GuideController : MonoBehaviour
{
    public enum GuideName
    {
        PLACE_SHIPS,
        DRAG_SHIPS
    }
    public static List<GuideName> activeGuides = new List<GuideName>();
    // The dictionary allows for retrieving data about each specific guide.
    // Currently, only a bool is stored (denoting whether the guide is active or not).
    // Since it is a static variable, we can track whether players have interacted with a guide before in the session.

    private void Start()
    {
        AddGuides();
    }

    public void AddGuides()
    {   // Add a guide to the array of each type. Uses Enum instead of String to add new values easier.
        foreach (GuideName guide in Enum.GetValues(typeof(GuideName))) {
            activeGuides.Add(guide);
        }
    }

    public void ToggleGuides()
    {
        if (activeGuides.Count > 0) activeGuides.Clear();

        else AddGuides();

        print(activeGuides.Count);
    }
}