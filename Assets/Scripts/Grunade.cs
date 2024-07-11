using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunade : MonoBehaviour, IPooledObject
{
    GameObject player;


    Rigidbody rb;
    public float projectileSpeed;
    public float knockback;
    public float knockbackRadius;
    //public enemyController enemy;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }


    public void OnObjectSpawn()
    {
        rb.AddForce(player.transform.forward * projectileSpeed);
    }


    void OnTriggerEnter(Collider other)
    {

        Collider[] cols = Physics.OverlapSphere(transform.position, knockbackRadius);
        for (int i = 0; i < cols.Length; i++)
        {

            if (cols[i].CompareTag("Enemy"))
            {
                enemyController e = cols[i].GetComponent<enemyController>();
                if (e != null)
                {
                    e.TakeDamage(100f);
                }
            }
            else if (cols[i].CompareTag("NPC"))
            {
                NPCScript n = cols[i].GetComponent<NPCScript>();
                if (n != null)
                {
                    n.TakeDamage(100f);
                }
            }
            else if (cols[i].CompareTag("PrisonOfficer"))
            {
                PrisonOfficer n = cols[i].GetComponent<PrisonOfficer>();
                if (n != null)
                {
                    n.TakeDamage(100f);
                }
            }
            else if (cols[i].CompareTag("Player"))
            {
                PlayerController n = cols[i].GetComponent<PlayerController>();
                if (n != null)
                {
                    n.TakeDamage(100f);
                }
            }
            Rigidbody rb = cols[i].GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(knockback, transform.position, knockbackRadius, 0f, ForceMode.Impulse);
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

        }
        gameObject.SetActive(false);
    }
}
