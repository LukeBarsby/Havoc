using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    GunBase gunBase;

    [Header("Shooting")]
    public GameObject Bullet;
    public float bulletSpeed = 500f;

    bool canFire;
    float shootTimer = 1f;
    float cooldown = 1f;

    void Start()
    {
        gunBase = GetComponent<GunBase>();
    }
    void Update()
    {

    }
    public void Shoot()
    {
        Debug.Log("Pew");
        gunBase.Use();
    }
}
