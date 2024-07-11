using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    public GameObject leftHand;
    public GameObject rightHand;

    Animator animator;
    Rigidbody rigidbody;
    private float speed;

    int numOfClicks;
    bool canClick;
    
    void Awake()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }
    void Start()
    {
        numOfClicks = 0;
        canClick = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && PlayerController.Instance.hasWeaponEquiped == false)
        {
            StartCombo();
        }
    }

    void StartCombo()
    {
        if (canClick)
        {
            numOfClicks++;
        }
        if (numOfClicks == 1)
        {
            animator.SetInteger("PunchingState", 1);
            PlayerController.Instance.isPunching = true;
        }
        if (!canClick && numOfClicks > 4)
        {
            canClick = true;
            numOfClicks = 0;
        }

    }

    public void ComboCheck()
    {
        canClick = false;

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("RightCross") && numOfClicks == 1)
        {
            //return to idle of only on click has happened since keyframe
            animator.SetInteger("PunchingState", 0);
            PlayerController.Instance.isPunching = false;
            canClick = true;
            numOfClicks = 0;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("RightCross") && numOfClicks >= 2)
        {
            //if animation is still playing and amount of clicks is > 1
            animator.SetInteger("PunchingState", 2);
            PlayerController.Instance.isPunching = true;
            canClick = true;
        }

        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("LeftJab") && numOfClicks == 2)
        {   
            // set back
            animator.SetInteger("PunchingState", 0);
            PlayerController.Instance.isPunching = false;
            canClick = true;
            numOfClicks = 0;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("LeftJab") && numOfClicks >= 3)
        {
            //if animation is still playing and amount of clicks is > 2
            animator.SetInteger("PunchingState", 3);
            PlayerController.Instance.isPunching = true;
            canClick = true;
        }

        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("RightHook") && numOfClicks == 3)
        {
            animator.SetInteger("PunchingState", 0);
            PlayerController.Instance.isPunching = false;
            canClick = true;
            numOfClicks = 0;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("RightHook") && numOfClicks >= 4)
        {
            animator.SetInteger("PunchingState", 4);
            PlayerController.Instance.isPunching = true;
            canClick = true;
        }

        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("UpperCut") || numOfClicks >= 4)
        {
            //end
            animator.SetInteger("PunchingState", 0);
            PlayerController.Instance.isPunching = false;
            canClick = true;
            numOfClicks = 0;
        }
    }

    public void RightColliderOn()
    {
        rightHand.SetActive(true);
    } 
    public void RightColliderOff()
    {
        rightHand.SetActive(false);
    } 
    public void LeftColliderOn()
    {
        leftHand.SetActive(true);
    }
    public void LeftColliderOff()
    {
        leftHand.SetActive(false);
    }
}
