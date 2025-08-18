using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [HideInInspector] public Character owner;
    public float damage;
    //[SerializeField] private Melee meleeAttack;
    [SerializeField] private ProjectileSpawner projectile;
    
    private void Awake()
    {
        owner = GetComponentInParent<Character>();
        owner.HeldWeapon = this;
    }

    public void SelectWeapon()
    {
        Awake();
    }

    public void Use()
    {
        // meleeAttack.Use();
        projectile?.Fire(this);
    }

    private void Update()
    {
        projectile?.OnUpdate();
    }
}
