using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class CameraScaleFixer : MonoBehaviour
{ 
    private void OnEnable()
    {
        float baseAspect = 16f / 9f;
        float screenAspect = (float)Screen.width / (float)Screen.height;  
        
        float scaleFactor = baseAspect / screenAspect;

        Camera cam = Camera.main;

        float baseOrthoSize = 15f;

        cam.orthographicSize = baseOrthoSize * scaleFactor;

    }
}
