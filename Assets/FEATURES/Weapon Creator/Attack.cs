using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Attack
{
    [HideInInspector] public Weapon weapon;

    public virtual void Apply(Weapon w)
    {
        weapon = w;
    }

}
