using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Attack
{
    [HideInInspector] public Weapon weapon;
    protected bool attacking = false;
    public float Cooldown = 0.2f;
    public float DamageMult = 1f;
    public float damage => Mathf.Floor(weapon.damage * DamageMult);
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
