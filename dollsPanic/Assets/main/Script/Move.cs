using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
    private float MovePow = 10;
    private Rigidbody rb;
    private bool MoveFlag;
    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
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
                rb.AddForce(transform.forward * MovePow);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                rb.AddForce(transform.forward * -MovePow);
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
