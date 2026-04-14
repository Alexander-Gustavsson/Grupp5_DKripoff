using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class Ability : ScriptableObject
{
    public new string name;
    public float cooldown;
    public bool isActive = false;
    public  virtual void Activate()
    {
       
    }
}
