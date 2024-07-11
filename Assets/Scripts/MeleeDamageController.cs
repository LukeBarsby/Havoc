using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDamageController : MonoBehaviour
{
    // Start is called before the first frame update
 
    void OnTriggerEnter(Collider other)
    {
        Damage(other);
    }
    void Damage(Collider other)
    {
        if (other.CompareTag("Melee"))
        {
            if (gameObject.tag == "Enemy")
            {
                enemyController e = GetComponent<enemyController>();
                MeleeBase m = other.GetComponentInParent<MeleeBase>();
                if (e != null)
                {
                    e.TakeDamage(100f);
                }
            }
            else if (gameObject.tag == "NPC")
            {
                NPCScript e = GetComponent<NPCScript>();
                MeleeBase m = other.GetComponentInParent<MeleeBase>();
                if (e != null)
                {
                    e.TakeDamage(100f);
                }
            }
            else if (gameObject.tag == "PrisonOfficer")
            {
                PrisonOfficer e = GetComponent<PrisonOfficer>();
                MeleeBase m = other.GetComponentInParent<MeleeBase>();
                if (e != null)
                {
                    e.TakeDamage(100f);
                }
            }
        }
        else if (other.CompareTag("Hands"))
        {
            if (gameObject.tag == "Enemy")
            {
                enemyController e = GetComponent<enemyController>();
                MeleeBase m = other.GetComponentInParent<MeleeBase>();
                if (e != null)
                {
                    e.TakeDamage(PlayerController.Instance.PlayerMeleeDamage);
                }
            }
            else if (gameObject.tag == "NPC")
            {
                NPCScript e = GetComponent<NPCScript>();
                MeleeBase m = other.GetComponentInParent<MeleeBase>();
                if (e != null)
                {
                    e.TakeDamage(PlayerController.Instance.PlayerMeleeDamage);
                }
            }
            else if (gameObject.tag == "PrisonOfficer")
            {
                PrisonOfficer e = GetComponent<PrisonOfficer>();
                
                e.TakeDamage(PlayerController.Instance.PlayerMeleeDamage);
                
            }
        }

    }
}
