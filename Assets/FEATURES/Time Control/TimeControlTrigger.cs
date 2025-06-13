using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TimeControlTrigger : MonoBehaviour
{
    [SerializeField] private float timeFactor;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        TimeScaleHandler obj = collision.gameObject.GetComponent<TimeScaleHandler>();
        if (obj != null)
        {
            obj.LocalTimeScale *= timeFactor;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        TimeScaleHandler obj = collision.gameObject.GetComponent<TimeScaleHandler>();
        if (obj != null)
        {
            obj.LocalTimeScale /= timeFactor;
        }
    }
}
