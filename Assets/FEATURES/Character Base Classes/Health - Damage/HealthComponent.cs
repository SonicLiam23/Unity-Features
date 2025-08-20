using System.Collections;
using UnityEngine;

public interface IDamageable
{
    void Damage(float dmg);
}

public class HealthComponent : MonoBehaviour, IDamageable
{
    private float maxHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] protected float defence = 0.0f;
    protected bool Invunerable => !hitbox.enabled;

    Collider2D hitbox;

    private void Awake()
    {
        maxHealth = currentHealth;
        hitbox = GetComponent<Collider2D>();
    }

    public virtual void Damage(float dmg)
    {
        if (Invunerable)
            return;

        Debug.Log("Hit");
        dmg = Mathf.Max(dmg - defence, 1.0f);
        currentHealth -= dmg;
        if (currentHealth <= 0f)
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
