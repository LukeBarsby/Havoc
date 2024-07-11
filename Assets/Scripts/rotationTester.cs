using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotationTester : MonoBehaviour
{
    public Quaternion rotate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = rotate;
    }
}
