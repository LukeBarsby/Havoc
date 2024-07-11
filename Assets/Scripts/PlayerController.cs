using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    Rigidbody rig;
    Animator anim;
    [Header("Setup")]
    public Camera cam;
    [SerializeField] GameObject crosshair;
    public Transform weaponSocket;

    [Header("Player Attributes")]
    [HideInInspector] public bool isDead;
    public float playerWantedLevel;
    [SerializeField] public float timer;
    [SerializeField] public float score;
    [SerializeField] float maxHealth;
    public float currentHealth;
    [SerializeField] float PlayerSpeed;
    [SerializeField] public float PlayerMeleeDamage;

    [Header("Mouse Stuff")]
    Ray cameraRay;
    float rayLength = 0f;

    [Header("Player Stuff")]
    Vector3 mousePos;
    Vector3 moveInput;
    Vector3 moveVelocity;
    private GameObject pickup;
    bool canGrab;
    float timeBetweenWeapons = 2f;
    float weaponTimer;
    float playerKnockback;
    bool movingforward;
    [HideInInspector] public bool hasWeaponEquiped;
    [HideInInspector] public bool isPunching;
    [SerializeField] float hpPickupAmount;
    [SerializeField] float scorePickupAmount;
    [SerializeField] float timePickupAmount;
    
    //anim shit
    float speed;
    string currentState;

    //Game mode
    [HideInInspector]public bool startTimer;
    [HideInInspector]public bool GameOver;
    float npcDeadCount;
    float copDeathCount;


    void Awake()
    {
        rig = GetComponent<Rigidbody>();
        cam = FindObjectOfType<Camera>();
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        GameOver = false;
        startTimer = false;
        isDead = false;
        isPunching = false;
        canGrab = true;
        hasWeaponEquiped = false;
        movingforward = false;
        score = 0f;
        currentHealth = maxHealth;
    }

    void Update()
    {
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * PlayerSpeed;

        cameraRay = cam.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 poiontToLook = cameraRay.GetPoint(rayLength);
            transform.LookAt(new Vector3(poiontToLook.x, transform.position.y, poiontToLook.z));

            if (poiontToLook.magnitude > 0)
            {
                crosshair.transform.position = poiontToLook += new Vector3(0,1,0);
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartFire();
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            EndFire();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            DropWeapon();
        }

        Vector3 velocity = rig.velocity;
        Vector3 localVel = transform.InverseTransformDirection(velocity);
        if (localVel.z > 0 )
        {
            movingforward = true;
        }
        else
        {
            movingforward = false;
        }

        if (startTimer)
        {
            Timer();
            npcDeadCount = SpawningNPCs.Instance.NPCDeathCount;
            copDeathCount = SpawningEnemies.Instance.EnemyDeathCount;
            score = npcDeadCount + copDeathCount;
        }
        if (isDead || timer <= 0)
        {
            GameOver = true;
        }

    }

    void Timer()
    {
        timer -= Time.deltaTime;
    }

    public void AddTime(float time)
    {
        timer += time;
    }

  

    void FixedUpdate()
    {
        if (isPunching)
        {
            rig.velocity = moveVelocity / 4;
        }
        else
        {
            if (movingforward)
            {
                rig.velocity = moveVelocity;
            }
            else
            {
                rig.velocity = moveVelocity / 2;
            }
        }
    }

    private void StartFire()
    {
        for (int i = 0; i < weaponSocket.childCount; i++)
        {
            Transform child = weaponSocket.GetChild(i);

            if (child.gameObject.activeSelf == true)
            {
                if (child.tag == "Range")
                {
                    GunBase gunBase = weaponSocket.GetComponentInChildren<GunBase>();
                    gunBase.StartFire();
                }
                if (child.tag == "Melee")
                {
                    MeleeBase meleeBase = weaponSocket.GetComponentInChildren<MeleeBase>();
                    meleeBase.Use();
                }
            }
        }
    }
    private void EndFire()
    {
        for (int i = 0; i < weaponSocket.childCount; i++)
        {
            Transform child = weaponSocket.GetChild(i);

            if (child.gameObject.activeSelf == true)
            {
                if (child.tag == "Range")
                {
                    GunBase gunBase = weaponSocket.GetComponentInChildren<GunBase>();
                    gunBase.EndFire();
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("Player"))
        {
            if (!hasWeaponEquiped)
            {
                string weaponToSwitchTo;

                weaponToSwitchTo = other.name;

                for (int i = 0; i < weaponSocket.childCount; i++)
                {
                    GameObject child = weaponSocket.GetChild(i).gameObject;
                    if (child.gameObject.name == weaponToSwitchTo)
                    {
                        child.SetActive(true);
                        hasWeaponEquiped = true;
                        UIController.Instance.GetCurrentWeaponIcon();

                        if (child.tag == "Range")
                        {
                            GunBase gunBase = weaponSocket.GetComponentInChildren<GunBase>();
                            gunBase.Start();
                            return;
                        }
                        if (child.tag == "Melee")
                        {
                            MeleeBase meleeBase = weaponSocket.GetComponentInChildren<MeleeBase>();
                            meleeBase.Start();
                            return;
                        }
                    }
                }

            }
            GiveAmmo(other);

            if (other.CompareTag("Health"))
            {
                currentHealth += hpPickupAmount;
                if (currentHealth >= 100)
                {
                    currentHealth = maxHealth;
                }
            }
            if (other.CompareTag("Ammo"))
            {
                GiveAmmo(other);
            }
            if (other.CompareTag("Score"))
            {
                score += scorePickupAmount;
            }
            if (other.CompareTag("WantedStar"))
            {
                playerWantedLevel--;
                if (playerWantedLevel <= 0)
                {
                    playerWantedLevel = 0;
                }
            }
            if (other.CompareTag("Timer"))
            {
                timer += timePickupAmount;
            }
        }
    }

    public void GiveAmmo(Collider other)
    {
        if (hasWeaponEquiped)
        {
            if (other.CompareTag("WeaponPickup"))
            {
                string weaponAmmo;
                weaponAmmo = other.name;

                for (int i = 0; i < weaponSocket.childCount; i++)
                {
                    GameObject child = weaponSocket.GetChild(i).gameObject;
                    if (child.activeSelf)
                    {
                        if (child.gameObject.name == weaponAmmo)
                        {
                            if (child.tag == "Range")
                            {
                                GunBase gunBase = weaponSocket.GetComponentInChildren<GunBase>();
                                gunBase.Ammo();
                            }
                            if (child.tag == "Melee")
                            {
                                MeleeBase meleeBase = weaponSocket.GetComponentInChildren<MeleeBase>();
                                meleeBase.Ammo();
                            }
                        }
                    }
                }

            }
        }
    }

    public void DropWeapon()
    {
        hasWeaponEquiped = false;
        SetBackWeaponAnim();
        weaponTimer = timeBetweenWeapons;
        for (int i = 0; i < weaponSocket.childCount; i++)
        {
            Transform child = weaponSocket.GetChild(i);

            if (child.gameObject.activeSelf == true)
            {
                child.gameObject.SetActive(false);
                UIController.Instance.GetCurrentWeaponIcon();
            }
        }
    }
    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
    }
    void OnAnimatorMove()
    {
        Vector3 vel = rig.velocity;
        speed = vel.magnitude;

        anim.SetFloat("Speed", speed);
    }

    public void Reload()
    {
        anim.Play("Reloading");
        return;
    }
    public void ChangeWeaponAnim(string animType)
    {
        currentState = animType;
        anim.SetBool(animType, true);
        return;
    }
    void SetBackWeaponAnim()
    {
        anim.SetBool(currentState, false);
        return;
    }

    public string GetWeaponIcon()
    {
        string iconType;
        if (hasWeaponEquiped)
        {
            for (int i = 0; i < weaponSocket.childCount; i++)
            {
                GameObject child = weaponSocket.GetChild(i).gameObject;
                if (child.activeSelf)
                {
                    if (child.tag == "Range")
                    {
                        GunBase gunBase = weaponSocket.GetComponentInChildren<GunBase>();
                        iconType = gunBase.ReturnWeaponIcon();
                        return iconType;
                    }
                    if (child.tag == "Melee")
                    {
                        MeleeBase meleeBase = weaponSocket.GetComponentInChildren<MeleeBase>();
                        iconType = meleeBase.ReturnWeaponIcon();
                        return iconType;
                    }
                    return null;
                }
            }
            return null;
        }
        else
        {
            iconType = "Fists";
            return iconType;
        }
    }
}
