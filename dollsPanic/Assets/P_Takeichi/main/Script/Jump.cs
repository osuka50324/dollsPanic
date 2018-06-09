using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    private float JumpPow = 400;
    private Rigidbody rb;
    private bool jump = false;
    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void JumpSet(float Num)
    {
        JumpPow *= Num;
    }
	
	// Update is called once per frame
	void Update ()
    {

        if (Input.GetKey(KeyCode.Space) && !jump)
        {
            rb.AddForce(Vector3.up * JumpPow);
            jump = true;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        jump = false;
    }
}
