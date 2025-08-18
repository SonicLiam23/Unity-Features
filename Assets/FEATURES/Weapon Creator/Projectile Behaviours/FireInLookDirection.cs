using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireInLookDirection : ProjectileBehaviour
{
    public override void OnCreate(ProjectileObject proj)
    {
        base.OnCreate(proj);

        float angle = Mathf.Atan2(ProjectileObj.Owner.LookVec.y, ProjectileObj.Owner.LookVec.x) * Mathf.Rad2Deg;
        ProjectileObj.transform.rotation = Quaternion.Euler(0, 0, angle);

        Vector2 newVel = ProjectileObj.Owner.LookVec * ProjectileObj.Speed;

        ProjectileObj.TimeScaler.SetDesiredVelocity(newVel);
    }
}
