using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCameraPreset", menuName = "Camera/CameraPreset")]
public class CameraPreset : ScriptableObject
{
    [Header("Core")]
    public Vector3 offset;

    [Header("Follow")]
    public float followSmoothTime = 0.3f; // Lower = faster response

    [Header("Look Ahead")]
    public float lookAheadDist = 2f;
    public float lookAheadReturnSpeed = 4f;  // Higher = snappier

    [Header("Vertical Deadzone")]
    public float verticalDeadzone = 0.75f; // in world units

    [Header("Shake")]
    public float shakeDuration = 0.15f;
    public float shakeStrength = 0.3f;
}
