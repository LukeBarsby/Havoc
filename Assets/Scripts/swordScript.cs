using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordScript : MonoBehaviour
{
    WeaponBase weaponBase;

    [Header("Melee")]
    public GameObject _sword;
    public Transform firePos;
    public Vector3 swordOffset;

    public void Swing()
    {
        //weaponBase.Melee(sword, firePos, swordOffset);
        GameObject sword = (GameObject)Instantiate(_sword, firePos.position ,gameObject.transform.rotation.normalized);
        sword.transform.parent = gameObject.transform;
        Destroy(sword, 1f);
    }
   
}
