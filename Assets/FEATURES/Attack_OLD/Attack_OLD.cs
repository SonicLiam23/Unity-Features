using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_OLD : MonoBehaviour
{

    public AttackManager manager;

    private void Awake()
    {
        manager = transform.parent.GetComponent<AttackManager>();
    }

    public void StopAttack()
    {
        manager.Attacking = false;
        manager.currTrigger.enabled = false;
        gameObject.SetActive(false);
    }
}
