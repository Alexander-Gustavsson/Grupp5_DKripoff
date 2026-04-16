using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[CreateAssetMenu(menuName = "Abilities/Multiple Targets")] //Tillåter oss att skapa instanser av denna klass i Unitys editor
public class AbilityMultipleTargets : Ability
{
    public int numberOfTargets = 3;


    private List<Vector2> selectedTargets = new List<Vector2>();

    public override void Activate()
    {
        selectedTargets.Clear();
        Debug.Log("Select " + numberOfTargets + " targets");
    }

    public override void Cancel()
    {
        selectedTargets.Clear();
        isActive = false;
    }
    //hmm
    public override void HandleClick(Vector2 pos)
    {
        if (selectedTargets.Contains(pos))
            return;

        selectedTargets.Add(pos);
        Debug.Log("Selected: " + pos);

        if (selectedTargets.Count >= numberOfTargets)
        {
            FireAll();
        }
    }

    private void FireAll()
    {
        if (gamePlay == null)
            return;

        // Temporarily disable ability so AIGridPressed doesn't re-route back into this ability
        bool wasActive = isActive;
        isActive = false;

        foreach (Vector2 pos in selectedTargets)
        {
            gamePlay.AIGridPressed(pos);
        }

        // Restore state (optional, but usually you want to turn it off after use)
        isActive = wasActive;

        // Clear targets after firing
        selectedTargets.Clear();

        // End ability (most cases you want this)
        isActive = false;
        gamePlay.inputLocked = false;
        abilityHolder.AbilityFinished();


    }

}
