using UnityEngine;
using static UnityEngine.InputSystem.OnScreen.OnScreenStick;

[System.Serializable]
public class MeleeObject : MonoBehaviour
{
    [HideInInspector] public Character Owner { get; private set; }

    public void OnAttack(Character newOwner)
    {
        Owner = newOwner;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Character>(out var other))
        {
            if (Owner.team == Team.NONE || Owner.team != other.team)
            {
                other.HealthComp.Damage(Owner.HeldWeapon.Melee.damage);
            }

        }
    }
}
