using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    [Header("Shooting")]
    public Transform firePos;
    public GameObject Bullet;
    public float bulletSpeed = 5;
    bool canFire;
    float shootTimer = 1f;
    float cooldown = 1f;

    [Header("Melee")]
    public GameObject Sword;
    public Vector3 swordOffset;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown > 0)
        {
            canFire = false;
            cooldown -= Time.deltaTime;
        }
        if (cooldown <= 0)
        {
            canFire = true;
        }

        if (Input.GetButton("Fire1") && canFire)
        {
            ShootArrow();
            cooldown = shootTimer;
        }

        if (Input.GetKey(KeyCode.Mouse1) && canFire)
        {
            Melee();
            cooldown = shootTimer;
        }
    }


  

    public void ShootArrow()
    {
        GameObject bullet = (GameObject)Instantiate(Bullet, firePos.position, Quaternion.identity);
        Rigidbody bRig = bullet.GetComponent<Rigidbody>();
        bRig.AddForce(firePos.forward * bulletSpeed);
        Destroy(bullet, 2.5f);

    }
    public void Melee()
    {
        GameObject sword = (GameObject)Instantiate(Sword, firePos.position + swordOffset, gameObject.transform.rotation.normalized) ;
        sword.transform.parent = gameObject.transform;
        Destroy(sword, 1f);
    }
}


