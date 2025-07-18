using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// controls the tag, saying if it is a perfect parry or not.
/// </summary>
public class Parry : MonoBehaviour
{
    [SerializeField] float perfectParryTime = 0.1f;

    private void OnEnable()
    {
        gameObject.tag = "PerfectParry";
        StartCoroutine(StartParry());
    }

    IEnumerator StartParry()
    {
        yield return new WaitForSecondsRealtime(perfectParryTime);
        gameObject.tag = "Parry";
    }
}
