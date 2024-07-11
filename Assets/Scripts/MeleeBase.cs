using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBase : WeaponBase
{
    [SerializeField]GameObject sharpBits;
    [SerializeField]float maxHits;
    [SerializeField] bool PlayerGun;
    [SerializeField] bool hands;
    [SerializeField] string animType;
    [SerializeField] string iconType;
    [SerializeField] string sound;
    [SerializeField] string reloadSound;
    public float damage;
    bool animationFinished;
    public float animationLength;
    float currentAnimationLength;


    float currentHits;
    bool startTimer;

    Animator anim;
    bool broken;
    void Awake()
    {
        animator = GetComponent<Animator>();
        currentHits = maxHits;
        currentAnimationLength = animationLength;
    }

    public void Start()
    {
        if (hands)
        {
            return;
        }
        if (PlayerGun)
        {
            PlayerController.Instance.ChangeWeaponAnim(animType);
        }
        sharpBits.SetActive(false);
        broken = false;
        animationFinished = true;
        Ammo();
    }
    void Update()
    {
        if (currentHits <= 0)
        {
            broken = true;
        }

        if (currentAnimationLength <= 0)
        {
            animationFinished = true;
        }
        currentAnimationLength -= Time.deltaTime;
    }
    public void Ammo()
    {
        AudioManager.Instance.PlaySound(reloadSound);
        PlayerController.Instance.Reload();
        animator.Play("GunReloading");
        animator.Play("Default");

        currentHits += maxHits;
        broken = false;
    }

    public override bool Use()
    {
        if (hands)
        {
            AudioManager.Instance.PlaySound(sound);
            return true;
        }
        if (!broken && animationFinished)
        {
            animator.Play("Swing");
            currentHits--;
            animationFinished = false;
            AudioManager.Instance.PlaySound(sound);
            currentAnimationLength = animationLength;
            startTimer = true;
            return true;
        }
        return false;
    }

    public string ReturnWeaponIcon()
    {
        return iconType;
    }
}
