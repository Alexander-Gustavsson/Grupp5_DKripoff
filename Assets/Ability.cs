using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public abstract class Ability : ScriptableObject
{
    public string abilityName;
    public float cooldown;
    public bool isActive = false;
    public abstract void Activate(); //abstrakt metod som måste implementeras i alla klasser som ärver från Ability.

}
