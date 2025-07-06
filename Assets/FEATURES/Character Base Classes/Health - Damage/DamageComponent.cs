using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DamageComponent : MonoBehaviour
{
    [SerializeField] private float damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        HealthBase healthComp = collision.GetComponent<HealthBase>();
        if (healthComp != null )
            healthComp.Damage(damage);
    }
}
