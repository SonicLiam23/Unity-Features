using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DamageComponent : MonoBehaviour
{
    public float damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        HealthComponent healthComp = collision.GetComponent<HealthComponent>();
        if (healthComp != null )
            healthComp.Damage(damage);
    }
}
