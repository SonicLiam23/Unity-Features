using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public interface IDamageable
{
    void Damage(float dmg);
}

public abstract class HealthBase : MonoBehaviour, IDamageable
{
    private readonly float maxHealth;
    [SerializeField] private float currentHealth;

    public HealthBase()
    {
        maxHealth = currentHealth;
    }

    public virtual void Damage(float dmg)
    {
        currentHealth -= dmg;
        if (currentHealth < 0)
        {
            OnDeath();
        }
    }  

    /// <summary>
    /// Heals this object by <paramref name="heal"/>
    /// </summary>
    /// <param name="heal"></param>
    public virtual void Heal(float heal)
    {
        currentHealth = Mathf.Min(currentHealth + heal, maxHealth);
    }

    protected virtual void OnDeath()
    {
        gameObject.SetActive(false);
    }
}
