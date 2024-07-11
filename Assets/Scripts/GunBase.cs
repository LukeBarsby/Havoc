using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : WeaponBase
{
    //ObjectPooler objectPooler;
    GameObject m_FirePoint;
    GameObject player;

    [Header("Auto shizz")]
    public float fireRate;
    float timer;
    bool fireButtonHeldDown;
    bool canFire;
    bool reloading;
    bool justShot;
    bool fireRateDelay;

    [Header("Gun shizz")]
    [SerializeField]public GameObject firePoint;
    [SerializeField] bool PlayerGun;
    [SerializeField] string animType;
    [SerializeField] string iconType;
    [SerializeField] string sound;
    [SerializeField] string reloadSound;
    [SerializeField] string pumpSound;
    [SerializeField] ParticleSystem muzzleFlash;
    public float totalAmmo;
    float currentAmmo;
    float currentMagSize;
    public float totalMagSize;
    public float totalReloadTime;
    float reloadTime;
    public bool shotgun;
    public bool throwingWeapon;
    public int pellets;
    Quaternion quat;
    List<Quaternion> l_Pellets;
    bool hasAmmo;


    public string bulletPool;

    //change to an override and call the base.awake
    void Awake()
    {
        if (shotgun)
        {
            l_Pellets = new List<Quaternion>(new Quaternion[pellets]);
            for (int i = 0; i < pellets; i++)
            {
                l_Pellets.Add(Quaternion.Euler(Vector3.zero));
            }
        }
        m_FirePoint = firePoint;
        animator = GetComponent<Animator>();
    }
    public void Start()
    {
        player = PlayerController.Instance.gameObject;
        if (PlayerGun)
        {
            PlayerController.Instance.ChangeWeaponAnim(animType);
        }
        reloadTime = totalReloadTime;
        fireButtonHeldDown = false;
        reloading = false;
        canFire = true;
        hasAmmo = false;
        timer = 0.0f;
        Ammo();
    }
    void Update()
    {
        if (!canFire && justShot)
        {
            justShot = false;
            timer = fireRate;
            fireRateDelay = true;
        }
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if (fireRateDelay && timer <= 0)
        {
            timer = 0;
            justShot = false;
            canFire = true;
        }

        if (currentMagSize <= 0)
        {
            if (hasAmmo)
            {
                StartCoroutine(Reloading());
                currentAmmo -= totalMagSize;
                currentMagSize = totalMagSize;
            }
            else
            {
                return;
            }
        }

        if (fireButtonHeldDown && canFire && !reloading)
        {
            if (currentAmmo <= 0 && currentMagSize <= 0)
            {
                //play need ammo
                canFire = false;
                hasAmmo = false;
            }
            else
            {
                if (shotgun)
                {
                    for (int i = 0; i < pellets; i++)
                    {
                        ObjectPooler.Instance.SpawnFromPool(bulletPool, m_FirePoint.transform.position, m_FirePoint.transform.rotation);
                    }
                    AudioManager.Instance.PlaySound(sound);
                    AudioManager.Instance.PlaySound(pumpSound);
                    muzzleFlash.Play();
                    currentMagSize--;
                    canFire = false;
                    justShot = true;
                }
                else if (throwingWeapon)
                {

                    ObjectPooler.Instance.SpawnFromPool(bulletPool, m_FirePoint.transform.position, m_FirePoint.transform.rotation);
                    AudioManager.Instance.PlaySound(sound);
                    currentMagSize--;
                    canFire = false;
                    justShot = true;
                }
                else if (currentMagSize > 0)
                {
                    ObjectPooler.Instance.SpawnFromPool(bulletPool, m_FirePoint.transform.position, m_FirePoint.transform.rotation);
                    AudioManager.Instance.PlaySound(sound);
                    muzzleFlash.Play();
                    currentMagSize--;
                    canFire = false;
                    justShot = true;
                }
            }
        }
    }
    IEnumerator Reloading()
    {
        reloading = true;
        if (PlayerGun)
        {
            PlayerController.Instance.Reload();
            AudioManager.Instance.PlaySound(reloadSound);
            animator.Play("GunReloading");
        }
        yield return new WaitForSeconds(reloadTime);
        if (PlayerGun)
        {
            animator.Play("Reloading");
        }
        reloading = false;
    }


    public override bool StartFire()
    {
        fireButtonHeldDown = true;
        return true;
    }

    public override bool EndFire()
    {
        fireButtonHeldDown = false;
        justShot = false;
        canFire = true;
        return true;
    }

    public void Ammo()
    {
        hasAmmo = true;
        currentAmmo += totalAmmo;
        StartCoroutine(Reloading());
    }

    public string ReturnWeaponIcon()
    {
        return iconType;
    }
}
