using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthSampler : MonoBehaviour
{
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        cam.depthTextureMode = DepthTextureMode.Depth;
    }
}
