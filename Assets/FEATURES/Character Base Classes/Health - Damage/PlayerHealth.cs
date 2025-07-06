using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : HealthBase
{
    [SerializeField] float defence = 0.0f;
    [SerializeField] float invunTime = 0.0f;

    public override void Damage(float dmg)
    {
        dmg = Mathf.Max(dmg - defence, 1.0f);

        base.Damage(dmg);
    }
}
 