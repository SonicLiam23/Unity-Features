using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class ProjectileBehaviour
{
    protected ProjectileObject ProjectileObj;
    public virtual void OnCreate(ProjectileObject proj)
    {
        ProjectileObj = proj;
    }
    public virtual void OnUpdate() { }
    public virtual void OnHit(Character victim) { }
    public virtual void OnDestroy() { }

}
