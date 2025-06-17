using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FollowCamera : MonoBehaviour
{
    [Header("Default preset will be the one placed at the top.")]
    [SerializeField] List<CameraPreset> cameraPresets; 

    Camera cam;
    private Vector3 velocity = Vector3.zero;

    [Header("Core")]
    public Transform TargetObject;
    private Vector3 offset;

    [Header("Follow")]
    private float followSmoothTime = 0.3f; // Lower = faster response

    [Header("Look Ahead")]
    private float lookAheadDist = 2f;
    float lookAheadReturnSpeed = 4f;  // Higher = snappier
    Vector3 currentLookAhead;
    float targetLastX;

    [Header("Vertical Deadzone")]
    float verticalDeadzone = 0.75f; // in world units

    [Header("Bounds (unset = no clamping")]
    Vector2 levelMin;
    Vector2 levelMax;

    [Header("Shake")]
    float shakeDuration = 0.15f;
    float shakeStrength = 0.3f;
    float shakeTimer;

    private void Awake()
    {
        TargetObject = GameObject.FindGameObjectWithTag("Player").transform;
        targetLastX = TargetObject.position.x;
        cam = GetComponent<Camera>();
        ChangePreset(cameraPresets[0].name);
    }

    void LateUpdate()
    {
        // Look-Ahead
        float deltaX = TargetObject.position.x - targetLastX;
        bool isMovingHorizontally = Mathf.Abs(deltaX) > 0.01f;

        if (isMovingHorizontally)
        {
            currentLookAhead.x = Mathf.Lerp(currentLookAhead.x, deltaX * lookAheadDist, Time.deltaTime * lookAheadReturnSpeed);
        }
        else
        {
            currentLookAhead.x = Mathf.Lerp(currentLookAhead.x, 0f, Time.deltaTime * lookAheadReturnSpeed);
        }
        targetLastX = TargetObject.position.x + offset.x;

        // vertical Deadzone
        float deltaY = TargetObject.position.y - transform.position.y;
        float yOffset = (Mathf.Abs(deltaY) > verticalDeadzone ? deltaY - Mathf.Sign(deltaY) * verticalDeadzone : 0f) + offset.y;

        // Calc desired position
        Vector3 desiredPos = new Vector3(TargetObject.position.x, transform.position.y + yOffset, transform.position.z) + currentLookAhead;

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

        TargetObject = newTarget;
        targetLastX = TargetObject.position.x;
    }
    public void TriggerShake(float magnitude = -1f, float duration = -1f)
    {
        shakeStrength = magnitude > 0f ? magnitude : shakeStrength;
        shakeDuration = duration > 0f ? duration : shakeDuration;
        shakeTimer = shakeDuration;
    }

    public void ChangePreset(string name)
    {
        foreach (CameraPreset preset in cameraPresets)
        {
            if (preset.name == name)
            {
                offset = preset.offset;
                followSmoothTime = preset.followSmoothTime;
                lookAheadDist = preset.lookAheadDist;
                lookAheadReturnSpeed = preset.lookAheadReturnSpeed;
                verticalDeadzone = preset.verticalDeadzone;
                shakeDuration = preset.shakeDuration;
                shakeStrength = preset.shakeStrength;
                return;
            }
        }
    }
    #endregion
}

