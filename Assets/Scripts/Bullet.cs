using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPooledObject
{
    Rigidbody rb;
    public float projectileSpeed;
    public float bulletDamage;
    public bool isSniper;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    public void OnObjectSpawn()
    {
        rb.AddForce(gameObject.transform.forward * projectileSpeed);


        //possibly add;
        //casingRbody.AddRelativeForce(new Vector3(1,1,0).normalized * ejectForce);

    }

    void OnTriggerEnter(Collider other)
    {
        Collider temp;
        temp = other;
        if (isSniper)
        {
            Damage(other);
            StartCoroutine(SniperBullet());
        }
        else
        {
            Damage(other);
            Reset();
        }
    }

    void Damage(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemyController e = other.GetComponent<enemyController>();
            if (e != null)
            {
                e.TakeDamage(bulletDamage);
            }
        }
        else if (other.CompareTag("NPC"))
        {
            NPCScript n = other.GetComponent<NPCScript>();
            if (n != null)
            {
                n.TakeDamage(bulletDamage);
            }
        } 
        else if (other.CompareTag("PrisonOfficer"))
        {
            PrisonOfficer n = other.GetComponent<PrisonOfficer>();
            if (n != null)
            {
                n.TakeDamage(bulletDamage);
            }
        }
        else if (other.CompareTag("Player"))
        {
            PlayerController n = other.GetComponent<PlayerController>();
            if (n != null)
            {
                n.TakeDamage(bulletDamage);
            }
        }
    }


    void Reset()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        gameObject.SetActive(false);
    }

    IEnumerator SniperBullet()
    {
        yield return new WaitForSeconds(1f);
        Reset();
    }
}
