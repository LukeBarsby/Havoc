using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    
    public float speed = 0.125f;

    void FixedUpdate()
    {
        Vector3 desired = target.position + offset;
        Vector3 smooth = Vector3.Lerp(transform.position, desired, speed);
        transform.position = smooth;

        transform.LookAt(target);
    }
}
