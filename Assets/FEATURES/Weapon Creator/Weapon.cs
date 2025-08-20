using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [HideInInspector] public Character owner;
    public float damage;
    public MeleeAttack Melee;
    public ProjectileSpawner Projectile;
    
    private void Awake()
    {
        owner = GetComponentInParent<Character>();
        Melee.MeleeObj = Instantiate(Melee.MeleeObj, transform);
        Melee.MeleeObj.gameObject.SetActive(false);
    }

    public void SelectWeapon()
    {
        owner.HeldWeapon = this;
    }

    public void Use()
    {
        Melee?.Attack(this);
        Projectile?.Fire(this);
    }

    private void Update()
    {
        Melee?.OnUpdate();
        Projectile?.OnUpdate();
    }
}
