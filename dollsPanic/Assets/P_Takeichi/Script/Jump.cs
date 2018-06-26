using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    private float JumpPow = 400;
    private Rigidbody rb;
    private bool jump = false;
    private Animator animator;
    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        animator = this.GetComponent<myBody>().Body.GetComponent<Animator>();
    }

    public void JumpSet(float Num)
    {
        JumpPow *= Num;
    }
	
	// Update is called once per frame
	void Update ()
    {

        if (Input.GetKeyDown(KeyCode.Space) && !jump)
        {
            Invoke("JumpOn",0.9f);
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

    void JumpOn()
    {
        rb.AddForce(Vector3.up * JumpPow);
    }
}
