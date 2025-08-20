using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.InputSystem.OnScreen.OnScreenStick;

/// <summary>
/// To be put on the projectile prefab to be spawned. Hitboxes and textures to be added manually
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(TimeScaleHandler))]
public class ProjectileObject : MonoBehaviour
{
    [HideInInspector] public Character Owner { get; private set; }
    [SerializeReference] List<ProjectileBehaviour> behaviours = new();

    public Rigidbody2D RBody { get; private set; }
    public TimeScaleHandler TimeScaler { get; private set; }
    public float Speed {  get; private set; }
    private Weapon weapon;

    private void Awake()
    {
        RBody = GetComponent<Rigidbody2D>();
        TimeScaler = GetComponent<TimeScaleHandler>();
    }

    public void OnSummon(Character newOwner, float newSpeed)
    {
        Owner = newOwner;
        Speed = newSpeed;
        weapon = Owner.HeldWeapon;
        transform.position = Owner.transform.position;
        foreach (ProjectileBehaviour behaviour in behaviours)
        {
            behaviour.OnCreate(this);
        }
    }

    private void Update()
    {
        foreach (ProjectileBehaviour behaviour in behaviours)
        {
            behaviour.OnUpdate();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Character>(out var other))
        {
            if (Owner.team == Team.NONE || Owner.team != other.team)
            {
                other.HealthComp.Damage(weapon.Projectile.damage);

                foreach (ProjectileBehaviour behaviour in behaviours)
                {
                    behaviour.OnHit(other);
                }
            }

        }

    }

    private void OnDestroy()
    {
        foreach (ProjectileBehaviour behaviour in behaviours)
        {
            behaviour.OnDestroy();
        }
    }
}
