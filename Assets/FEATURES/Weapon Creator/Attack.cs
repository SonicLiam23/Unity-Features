using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Attack
{
    [HideInInspector] public Weapon weapon;
    protected bool attacking = false;
    public float Cooldown;
    /// <summary>
    /// The cooldown between Starting attacking, and starting attacking again
    /// </summary>
    protected float currentCooldown;

    public virtual bool CanAttack()
    {
        return (currentCooldown <= 0f && attacking == false);
    }

    public virtual void OnUpdate()
    {
        if (Cooldown > 0f)
        {
            currentCooldown -= Time.deltaTime;
        }
    }
}
