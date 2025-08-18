using UnityEngine;

[System.Serializable]
public class ProjectileSpawner : Attack
{
    public ProjectileObject ProjectileObj;
    [HideInInspector] public Weapon FiringWeapon;
    public float Cooldown, LifeTime, TimeBetweenShots, ProjectileSpeed;
    public int FireCount = 1;
    private int fired = 0;
    private bool firing = false;

    /// <summary>
    /// The cooldown between Starting firing, and starting firing again
    /// </summary>
    private float currentCooldown;
    /// <summary>
    /// if FireCount is > 1, this is the current timer between each shot.
    /// </summary>
    private float currentMultishotCooldown;


    public void Fire(Weapon w)
    {
        if (CanFire())
        {
            FiringWeapon = w;
            firing = true;
        }
    }

    /// <summary>
    /// returns true when all shots have been fired (in a multishot) or when the cooldown is over, whichever is longest.
    /// </summary>
    /// <returns>true: if both all shots have been fired, and cooldown is over.</returns>
    public bool CanFire()
    {
        // fired will equal 0 when we are yet to fire anything
        return (fired <= 0 && currentCooldown <= 0f);
    }

    public void OnUpdate()
    {
        if (Cooldown > 0f)
        {
            currentCooldown -= Time.deltaTime;
        }

        if (firing)
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

                    go.OnSummon(FiringWeapon.owner, ProjectileSpeed);
                }
            }
            else
            {
                firing = false;
                fired = 0;
            }
        }
    }    
}
