using UnityEngine;

[System.Serializable]
public class ProjectileSpawner : Attack
{
    [Tooltip("Prefab with ProjectileObject goes here")]
    public ProjectileObject ProjectileObj;
    public float LifeTime, TimeBetweenShots, ProjectileSpeed;
    public int FireCount = 1;
    private int fired = 0;
    


    /// <summary>
    /// if FireCount is > 1, this is the current timer between each shot.
    /// </summary>
    private float currentMultishotCooldown = 0f;


    public void Fire(Weapon w)
    {
        if (CanAttack())
        {
            weapon = w;
            attacking = true;
            currentCooldown = Cooldown;
        }
    }

    /// <summary>
    /// returns true when all shots have been fired (in a multishot) or when the cooldown is over, whichever is longest.
    /// </summary>
    /// <returns>true: if both all shots have been fired, and cooldown is over.</returns>
    public override bool CanAttack()
    {
        return base.CanAttack() && fired <= 0;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        // i dont like this
        if (attacking)
        {
            if (fired < FireCount)
            {
                currentMultishotCooldown -= Time.deltaTime;

                if (currentMultishotCooldown <= 0f)
                {
                    currentMultishotCooldown = TimeBetweenShots;
                    fired++;

                    ProjectileObject go = Object.Instantiate(ProjectileObj);
                    if (LifeTime > 0f)
                        Object.Destroy(go.gameObject, LifeTime);

                    go.OnSummon(weapon.owner, ProjectileSpeed);
                }
            }
            else
            {
                attacking = false;
                fired = 0;
                currentMultishotCooldown = 0f;
            }
        }
    }    
}
