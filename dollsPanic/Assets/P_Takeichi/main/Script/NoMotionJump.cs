using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoMotionJump : MonoBehaviour
{
    private float JumpPow = 400;
    private Rigidbody rb;
    private bool jump = false;
    public GameObject model;
    private Animator animator;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = this.GetComponent<myBody>().Body.GetComponent<Animator>();
    }

    public void JumpSet(float Num)
    {
        JumpPow *= Num;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && !jump)
        {
            rb.AddForce(Vector3.up * JumpPow);
            jump = true;
            animator.SetTrigger("OnJump");
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (jump)
        {
            jump = false;
            animator.SetTrigger("OnJumpEnd");
        }
    }
}