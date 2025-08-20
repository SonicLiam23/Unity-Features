using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class MeleeAttack : Attack
{
    // doesnt need to spawn extra things like projectile
    public MeleeObject MeleeObj;
    [SerializeField] private float distanceFromPlayer = 3f;

    [Tooltip("Reccomended to be the same as the animation time.")]
    [SerializeField] private float useTime;
    private float currentUseTime;

    public override bool CanAttack()
    {
        attacking = currentCooldown > 0f;
        return !attacking;
    }
    public override void OnUpdate()
    {
        base.OnUpdate();    

        if (currentUseTime >= 0f)
            currentUseTime -= Time.deltaTime;
        else
            MeleeObj.gameObject.SetActive(false);
            
    }

    public void Attack(Weapon w)
    {
        if (CanAttack())
        {
            weapon = w;
            currentUseTime = useTime;
            currentCooldown = Cooldown;
            attacking = true;

            float angle = Mathf.Atan2(weapon.owner.LookVec.y, weapon.owner.LookVec.x) * Mathf.Rad2Deg;
            MeleeObj.transform.rotation = Quaternion.Euler(0, 0, angle);
            MeleeObj.transform.localPosition = weapon.owner.LookVec * distanceFromPlayer;
            MeleeObj.OnAttack(weapon.owner);
            MeleeObj.gameObject.SetActive(true);
        }

    }


}
