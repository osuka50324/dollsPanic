using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
    private float MovePow = 1;
    private Rigidbody rb;
    private bool MoveFlag;
    private Animator animator;
    private Vector3 Stop;
    private float MaxSpeed;
    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        animator = this.GetComponent<myBody>().Body.GetComponent<Animator>();
        MaxSpeed = this.GetComponent<myBody>().MaxSpeed;
    }

    public void MoveSet(float Num)
    {
        MovePow *= Num;
    }

    // Update is called once per frame
    void Update ()
    {
        if (MoveFlag)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rb.AddForce(transform.forward * MovePow, ForceMode.Impulse);
                if(rb.velocity.magnitude > MaxSpeed)
                {
                    rb.velocity = rb.velocity.normalized * MaxSpeed;
                }
                animator.SetBool("OnWalk", true);
            }else
            {
                Stop = rb.velocity;
                Stop.x = Stop.z = 0;
                rb.velocity = Stop;
                animator.SetBool("OnWalk", false);
            }
        }else
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rb.AddForce(transform.forward * MovePow / 5, ForceMode.Impulse);
                Stop = rb.velocity;
                Stop.y = 0;
                if (rb.velocity.magnitude > MaxSpeed / 2)
                {
                    Stop = Stop.normalized * MaxSpeed / 2;
                }
                Stop.y = rb.velocity.y;
                rb.velocity = Stop;
                animator.SetBool("OnWalk", true);
            }else
            {
                Stop = rb.velocity;
                Stop.x = Stop.z = 0;
                rb.velocity = Stop;
                animator.SetBool("OnWalk", false);
            }
        }
    }
    void OnCollisionExit(Collision col)
    {
        MoveFlag = false;
    }
    void OnCollisionEnter(Collision col)
    {
        MoveFlag = true;
    }
}
