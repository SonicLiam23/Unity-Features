using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : HealthBase
{
    [SerializeField] float defence = 0.0f;
    [SerializeField] float invunTime = 0.0f;
    bool invunerable = false;

    public override void Damage(float dmg)
    {
        if (invunerable)
            return;

        dmg = Mathf.Max(dmg - defence, 1.0f);
        StartCoroutine(Invunerable());
        base.Damage(dmg);
    }

    IEnumerator Invunerable()
    {
        invunerable = true;
        yield return new WaitForSeconds(invunTime);
        invunerable = false;
    }
}
 