using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// controls the tag, saying if it is a perfect parry or not.
/// </summary>
public class Parry : MonoBehaviour
{

    public AttackManager manager;

    private void Awake()
    {
        manager = transform.parent.GetComponent<AttackManager>();
    }


    private void OnEnable()
    {
        gameObject.tag = "PerfectParry";
        
    }


    public void EndPerfectParry()
    {
        gameObject.tag = "Parry";
    }

    public void StopParry()
    {
        manager.Attacking = false;
        manager.currTrigger.enabled = false;
        gameObject.SetActive(false);
    }

}
