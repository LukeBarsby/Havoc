using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimController : MonoBehaviour
{
    Animator animator;
    Rigidbody rigidbody;
    private float speed;
    void Start()
    {
       // StartCoroutine(CalcVelocity());
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //var vel = rigidbody.velocity;      
        //speed = vel.magnitude;


        //animator.SetFloat("Speed", speed);
        //Debug.Log(speed);
    }

    void OnAnimatorMove()
    {
        Vector3 vel = rigidbody.velocity;
        speed = vel.magnitude;

        animator.SetFloat("Speed", speed);
    }


    //IEnumerator CalcVelocity()
    //{
    //    while (Application.isPlaying)
    //    {
    //        var prevPos = transform.position;
    //        yield return new WaitForFixedUpdate();
    //        var currVel = (prevPos - transform.position).magnitude / Time.deltaTime;
    //        animator.SetFloat("Speed", currVel);
    //        Debug.Log(currVel);
    //    }
    //}




}
