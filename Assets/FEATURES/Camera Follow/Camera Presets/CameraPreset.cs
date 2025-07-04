using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCameraPreset", menuName = "Camera/CameraPreset")]
public class CameraPreset : ScriptableObject
{
    [Header("Core")]
    public Vector3 offset;

    [Header("Follow")]
    [Tooltip("Lower = faster response")]
    public float followSmoothTime = 0.3f;

    [Header("Look Ahead")]
    [Tooltip("Higher = snappier")]
    public float lookAheadDist = 2f;
    public float lookAheadReturnSpeed = 4f;

    [Header("Vertical Deadzone")]
    [Tooltip("In world units")]
    public float verticalDeadzone = 0.75f;

    [Header("Shake")]
    public float shakeDuration = 0.15f;
    public float shakeStrength = 0.3f;
}
