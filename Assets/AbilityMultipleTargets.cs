using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[CreateAssetMenu(menuName = "Abilities/Multiple Targets")] //Tillåter oss att skapa instanser av denna klass i Unitys editor
public class AbilityMultipleTargets : Ability
{
    public int numberOfTargets;

    public override void Activate()
    {
        //Ability som tillåter spelaren att klicka på flera rutor och sedan skjuta
    }
}
