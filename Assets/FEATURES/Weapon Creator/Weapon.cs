using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [HideInInspector] public Character owner;
    public float damage;
    [SerializeField] private MeleeAttack melee;
    [SerializeField] private ProjectileSpawner projectile;
    
    private void Awake()
    {
        owner = GetComponentInParent<Character>();
        melee.MeleeObj = Instantiate(melee.MeleeObj, transform);
        melee.MeleeObj.gameObject.SetActive(false);
    }

    public void SelectWeapon()
    {
        owner.HeldWeapon = this;
    }

    public void Use()
    {
        melee?.Attack(this);
        projectile?.Fire(this);
    }

    private void Update()
    {
        melee?.OnUpdate();
        projectile?.OnUpdate();
    }
}
