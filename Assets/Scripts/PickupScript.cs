using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    public float pickupTimer;
    public Vector3 moveLocation;
    Vector3 pickupPlace;

    float currentTime;

    void Start()
    {
        pickupPlace = transform.position;
    }

    void Update()
    {
        if (currentTime <= 0)
        {
            PickupPlace();
        }
        currentTime -= Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MoveLocation();
            currentTime = pickupTimer;
        }
    }

    void MoveLocation()
    {
        gameObject.transform.position = moveLocation;
    } 
    void PickupPlace()
    {
        gameObject.transform.position = pickupPlace;
    }


}
