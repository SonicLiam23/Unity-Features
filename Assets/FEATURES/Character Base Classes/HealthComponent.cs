using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    private readonly float maxHealth;
    [SerializeField] private float health;
    /// <summary>
    /// Damage taken is reduced by defence to a minimum of 10% of damage taken
    /// </summary>
    [SerializeField] private float defence;
    [SerializeField] private bool invunerable = false;

    public HealthComponent()
    {
        maxHealth = health;
    }

    public void Damage(float dmg)
    {
        float dmgTaken = Mathf.Max(0.1f * dmg, dmg - defence);
        dmgTaken *= invunerable ? 0f : 1f;
        health -= dmgTaken;
    }  

    /// <summary>
    /// Heals this object by <paramref name="heal"/>
    /// </summary>
    /// <param name="heal"></param>
    public void Heal(float heal)
    {
        health = Mathf.Min(health + heal, maxHealth);
    }
}
