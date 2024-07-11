using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBullet : MonoBehaviour, IPooledObject
{
    GameObject player;


    Rigidbody rb;
    public float projectileSpeed;
    public float spreadAngle;
    public float damage;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }


    public void OnObjectSpawn()
    {
        Quaternion temp = Random.rotation;
        gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, temp, spreadAngle);
        rb.AddForce(gameObject.transform.forward * projectileSpeed);
        StartCoroutine(StopBullet());
    }

    IEnumerator StopBullet()
    {
        yield return new WaitForSeconds(2);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemyController e = other.GetComponent<enemyController>();
            if (e != null)
            {
                e.TakeDamage(damage);
            }
        }
        else if (other.CompareTag("NPC"))
        {
            NPCScript n = other.GetComponent<NPCScript>();
            if (n != null)
            {
                n.TakeDamage(damage);
            }
        }
        else if (other.CompareTag("PrisonOfficer"))
        {
            PrisonOfficer n = other.GetComponent<PrisonOfficer>();
            if (n != null)
            {
                n.TakeDamage(damage);
            }
        }
        else if (other.CompareTag("Player"))
        {
            PlayerController n = other.GetComponent<PlayerController>();
            if (n != null)
            {
                n.TakeDamage(damage);
            }
        }
    }
}
