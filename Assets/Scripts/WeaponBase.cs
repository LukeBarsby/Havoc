using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    //Animator animator;
    protected Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    #region OldCode
    //    //[Header("Shooting")]
    //    //public Transform firePos;
    //    //public GameObject Bullet;
    //    //public float bulletSpeed = 5;
    //    //bool canFire;
    //    //float shootTimer = 1f;
    //    //float cooldown = 1f;

    //    //[Header("Melee")]
    //    //public GameObject Sword;
    //    //public Vector3 swordOffset;

    //    public void Shoot(GameObject bulletPrefab, Transform firePos, float bulletSpeed)
    //    {
    //        GameObject bullet = (GameObject)Instantiate(bulletPrefab, firePos.position, Quaternion.identity);
    //        Rigidbody bRig = bullet.GetComponent<Rigidbody>();
    //        bRig.AddForce(firePos.forward * bulletSpeed);
    //        Destroy(bullet, 2.5f);
    //    }

    //    public void Melee(GameObject meleePrefab, Transform firePos, Vector3 offset)
    //    {
    //        GameObject sword = (GameObject)Instantiate(meleePrefab, firePos.position + offset, gameObject.transform.rotation.normalized);
    //        sword.transform.parent = gameObject.transform;
    //        Destroy(sword, 1f);
    //    }

    #endregion

    //get a ref to animator in awake and store it in a protected member variable

    public virtual bool Use()
    {
        return true;
    }
    public virtual bool StartFire()
    {
        return true;
    }
    public virtual bool EndFire()
    {
        return true;
    }
}
