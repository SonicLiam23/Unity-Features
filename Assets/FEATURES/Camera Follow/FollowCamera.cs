using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FollowCamera : MonoBehaviour
{

    [SerializeField] List<ScriptableObject> cameraPresets; 

    Camera cam;
    private Vector3 velocity = Vector3.zero;

    [Header("Core")]
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    [Header("Follow")]
    [SerializeField] private float followSmoothTime = 0.3f; // Lower = faster response

    [Header("Look Ahead")]
    [SerializeField] private float lookAheadDist = 2f;
    [SerializeField] float lookAheadReturnSpeed = 4f;  // Higher = snappier
    Vector3 currentLookAhead;
    float targetLastX;

    [Header("Vertical Deadzone")]
    [SerializeField] float verticalDeadzone = 0.75f; // in world units

    [Header("Bounds (unset = no clamping")]
    [SerializeField] Vector2 levelMin;
    [SerializeField] Vector2 levelMax;

    [Header("Shake")]
    [SerializeField] float shakeDuration = 0.15f;
    [SerializeField] float shakeStrength = 0.3f;
    float shakeTimer;



    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        targetLastX = target.position.x;
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        // Look-Ahead
        float deltaX = target.position.x - targetLastX;
        bool isMovingHorizontally = Mathf.Abs(deltaX) > 0.01f;

        if (isMovingHorizontally)
        {
            currentLookAhead.x = Mathf.Lerp(currentLookAhead.x, deltaX * lookAheadDist, Time.deltaTime * lookAheadReturnSpeed);
        }
        else
        {
            currentLookAhead.x = Mathf.Lerp(currentLookAhead.x, 0f, Time.deltaTime * lookAheadReturnSpeed);
        }
        targetLastX = target.position.x + offset.x;

        // vertical Deadzone
        float deltaY = target.position.y - transform.position.y;
        float yOffset = (Mathf.Abs(deltaY) > verticalDeadzone ? deltaY - Mathf.Sign(deltaY) * verticalDeadzone : 0f) + offset.y;

        // Calc desired position
        Vector3 desiredPos = new Vector3(target.position.x, transform.position.y + yOffset, transform.position.z) + currentLookAhead;

        // smooth follow
        Vector3 smoothed = Vector3.SmoothDamp(transform.position, desiredPos, ref velocity, followSmoothTime);

        // clamp to the set level bounds
        if (levelMax != levelMin)
        {
            float camHeight = cam.orthographicSize;
            float camWidth = camHeight * cam.aspect;

            smoothed.x = Mathf.Clamp(smoothed.x, levelMin.x + camWidth, levelMax.x - camWidth);
            smoothed.y = Mathf.Clamp(smoothed.y, levelMin.y + camHeight, levelMax.y - camHeight);
        }

        // apply shake
        if (shakeTimer > 0f)
        {
            shakeTimer -= Time.deltaTime;
            smoothed += (Vector3)Random.insideUnitCircle * shakeStrength;
        }

        // and finally... set the camera position
        transform.position = smoothed;
    }

    #region API
    public void SetTarget(Transform newTarget, float newSmoothTime = -1f)
    {
        if (newSmoothTime > 0f)
        {
            followSmoothTime = newSmoothTime;
        }

        target = newTarget;
        targetLastX = target.position.x;
    }
    public void TriggerShake(float magnitude = -1f, float duration = -1f)
    {
        shakeStrength = magnitude > 0f ? magnitude : shakeStrength;
        shakeDuration = duration > 0f ? duration : shakeDuration;
        shakeTimer = shakeDuration;
    }
    #endregion
}

