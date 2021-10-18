using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform target;

    private void FixedUpdate()
    {
        transform.position = target.position;
    }
}
