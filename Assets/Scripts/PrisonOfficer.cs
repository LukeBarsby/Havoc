using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrisonOfficer : MonoBehaviour
{
    public float attackRangeRadius;

    [HideInInspector] public BoxCollider col;
    [HideInInspector] public Animator anim;
    [HideInInspector] public Rigidbody rb;

    //Waypoints config
    public Transform[] waypoints;
    [SerializeField] float speed;
    [SerializeField] float runSpeed;
    [HideInInspector] public int index;
    Vector3 lookDir;
    Vector3 rotation;
    Quaternion face;

    //PanicConfig
    [HideInInspector] bool panic;
    [SerializeField] float rayRange;
    [SerializeField] Transform rayPos;

    [SerializeField] float maxHealth;
    float currentHealth;
    bool dead;

    void Awake()
    {
        col = GetComponent<BoxCollider>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        dead = false;
        panic = false;
        index = 0;
        transform.position = waypoints[index].transform.position;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject currentTarget = FindPlayer();
        if (currentTarget != null)
        {
            panic = true;
        }
        if (!dead)
        {
            if (!panic)
            {
                Roam();
            }
            else
            {
                Panic();
            }
        }
    }

    public GameObject FindPlayer()
    {
        GameObject player;
        player = GameObject.FindGameObjectWithTag("Player");
        GameObject inRange = null;
        Vector3 position = transform.position;
        Vector3 difference = player.transform.position - position;
        float curDistance = difference.magnitude;
        if (curDistance <= attackRangeRadius)
        {
            inRange = player;
        }
        return inRange;
    }

    public void TakeDamage(float dmg)
    {
        panic = true;
        currentHealth -= dmg;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        dead = true;
        anim.Play("Dead");
        Destroy(gameObject, 5);
        col.enabled = false;
        rb.isKinematic = true;
    }
    void Roam()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypoints[index].transform.position, speed * Time.deltaTime);

        lookDir = waypoints[index].transform.position - transform.position;
        if (lookDir != Vector3.zero)
        {
            face = Quaternion.LookRotation(lookDir);
        }
        rotation = Quaternion.Lerp(transform.rotation, face, Time.deltaTime * 5f).eulerAngles;
        transform.rotation = Quaternion.Euler(0, rotation.y, 0);

        anim.SetFloat("Speed", 2);


        if (transform.position == waypoints[index].transform.position)
        {
            index += 1;
        }

        if (index == waypoints.Length)
        {
            index = 0;
        }
    }
    void Panic()
    {
        RaycastHit hit;
        if (Physics.Raycast(rayPos.position, transform.TransformDirection(Vector3.forward), out hit, rayRange))
        {
            Debug.DrawRay(rayPos.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            Quaternion newRotation;
            newRotation = UnityEngine.Random.rotation;
            transform.rotation = Quaternion.Euler(0f, newRotation.eulerAngles.y, 0f);
        }
        else
        {
            Debug.DrawRay(rayPos.position, transform.TransformDirection(Vector3.forward) * rayRange, Color.white);
            //move to the end of the ray
            transform.position += transform.forward * Time.deltaTime * runSpeed;
            anim.SetFloat("Speed", 3);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRangeRadius);
    }
}
