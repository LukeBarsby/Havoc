using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using System;

public class enemyController : MonoBehaviour
{
    SpawningEnemies spawningEnemies;

    [Header("Stats")]
    public float maxHealth = 100.0f;
    public float health;
    public float turnSpeed = 5f;

    public float attackRangeRadius;

    NavMeshAgent agent;
    public Transform target;
    Vector3 lookDir;
    Quaternion face;
    Vector3 rotation;
    bool inRange;
    bool startShouting;

    Rigidbody rigidbody;
    Animator animator;
    private float speed;

    [Header("KnockBack")]
    public bool knockBack;
    public float knockBackPower;
    public float knockBackTimer;
    public Vector3 direction;
    float originalSpeed;
    float originalAngularSpeed;
    float originalAcceleration;
    public float newSpeed;
    public float newAngularSpeed;
    public float newAcceleration;

    [Header("Sound")]
    bool playSound;
    float timer;

    GameObject player;
    [SerializeField] Transform weaponSocket;

    [Header("CharacterModel")]
    [SerializeField] List<GameObject> CharacterModels = new List<GameObject>();
    int randomCharacter;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        target = PlayerController.Instance.transform;
        player = PlayerController.Instance.gameObject;

        randomCharacter = UnityEngine.Random.Range(0, CharacterModels.Count);
        CharacterModels[randomCharacter].SetActive(true);

        health = maxHealth;
        inRange = false;
        knockBack = false;

        originalSpeed = agent.speed;
        originalAngularSpeed = agent.angularSpeed;
        originalAcceleration = agent.acceleration;
        startShouting = true;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject currentTarget = FindClosestTarget();
        if (currentTarget != null)
        {
            inRange = true;
        }
        else
        {
            inRange = false;
        }

        lookDir = target.position - agent.transform.position;
        face = Quaternion.LookRotation(lookDir);
        rotation = Quaternion.Lerp(agent.transform.rotation, face, Time.deltaTime * 5f).eulerAngles;
        agent.transform.rotation = Quaternion.Euler(0, rotation.y, 0);

        agent.stoppingDistance = attackRangeRadius;
        agent.SetDestination(target.position);

        if (inRange)
        {
            startShouting = true;
            animator.SetFloat("Speed", 0);
            Attack();
        }
        else if (!inRange)
        {
            startShouting = false;
            StopAttack();
            Vector3 vel = rigidbody.velocity;
            speed = vel.magnitude;
            animator.SetFloat("Speed", speed);
        }

        if (timer <= 0 && startShouting)
        {
            playSound = true;
            timer += 10;
        }
        else
        {
            playSound = false;
        }
        timer -= Time.deltaTime;


    }

    void FixedUpdate()
    {
        if (knockBack)
        {
            agent.velocity = direction * knockBackPower;
        }
    }

    public GameObject FindClosestTarget()
    {
        GameObject[] player;
        player = GameObject.FindGameObjectsWithTag("Player");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject p_player in player)
        {
            Vector3 difference = p_player.transform.position - position;
            float curDistance = difference.magnitude;

            if (curDistance < distance && curDistance <= attackRangeRadius)
            {
                closest = p_player;
                distance = curDistance;
            }
        }

        return closest;
    }

    private void Attack()
    {
        float random = UnityEngine.Random.Range(0, 1);
        if (random < 1 && playSound)
        {
            AudioManager.Instance.PlayRandomPoliceSound();
        }
        GunBase gunBase = weaponSocket.GetComponentInChildren<GunBase>();
        gunBase.StartFire();
    }
    private void StopAttack()
    {
        GunBase gunBase = weaponSocket.GetComponentInChildren<GunBase>();
        gunBase.EndFire();
    }

    public void TakeDamage(float dmg)
    {

        health -= dmg;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        SpawningEnemies.Instance.RemoveFromList(gameObject.name);
        Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRangeRadius);
    }

    IEnumerator Knockback()
    {
        agent.speed = newSpeed;
        agent.angularSpeed = newAngularSpeed;
        agent.acceleration = newAcceleration;
        knockBack = true;

        yield return new WaitForSeconds(knockBackTimer);

        knockBack = false;
        agent.speed = originalSpeed;
        agent.angularSpeed = originalAngularSpeed;
        agent.acceleration = originalAcceleration;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            direction = player.transform.forward; //Always knocks ememy in the direction the main character is facing
            StartCoroutine(Knockback());
        }
    }
}
