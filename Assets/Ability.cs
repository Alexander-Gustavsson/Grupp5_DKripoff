using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public abstract class Ability : ScriptableObject
{
    public string abilityName;
    public float cooldown;
    public bool isActive = false;

    protected GamePlay gamePlay;

    public void SetGamePlay(GamePlay gp)
    {
        gamePlay = gp;
    }

    protected AbilityHolder abilityHolder;

    public void SetAbilityHolder(AbilityHolder holder)
    {
        abilityHolder = holder;
    }

    public abstract void Activate(); //abstrakt metod som måste implementeras i alla klasser som ärver från Ability.

    public virtual void Cancel()
    {
    }

    public virtual void HandleClick(Vector2 pos)
    {
        // default: do nothing
    }

}
