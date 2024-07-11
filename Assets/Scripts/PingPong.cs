using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPong : MonoBehaviour
{
  
    public float yCenter = 1f;

    void Start()
    {
 
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, yCenter + Mathf.PingPong(Time.time, 1f), transform.position.z);
        transform.Rotate(0, 75f * Time.deltaTime, 0);
    }

}
