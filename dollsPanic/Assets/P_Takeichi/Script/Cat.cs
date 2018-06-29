using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;
    private Vector3 Stop;
    private Vector3 rightRotate = new Vector3(0, 5, 0);
    private Vector3 leftRotate = new Vector3(0, -5, 0);

    private int Mode = 0;
    private float ModeTime = 0;
    private bool rotate;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = this.transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ModeTime -= Time.deltaTime;
        if(ModeTime < 0)
        {
            if(Mode == 0)
            {
                Mode = Random.Range(0, 10);
                if(Mode == 2)
                {
                    ModeTime = 0.1f * Random.Range(1,3);
                    rotate = Random.Range(0, 2) == 0 ? true:false;
                }else
                {
                    ModeTime = 1.0f;
                    Mode = 0;
                }
            }else
            {
                Mode --;
                ModeTime = 1.0f;
                    Stop = rb.velocity;
                    Stop.x = Stop.z = 0;
                    rb.velocity = Stop;
                    animator.SetBool("OnWalk", false);
            }
        }

        switch (Mode)
        {
            case 1:
                
                
                RaycastHit hit;
                if (Physics.Raycast(this.transform.position, this.transform.forward * 6, out hit, 6))
                {
                    Mode = 0;
                    ModeTime = 0.0f;
                    Stop = rb.velocity;
                    Stop.x = Stop.z = 0;
                    rb.velocity = Stop;
                    animator.SetBool("OnWalk", false);
                    break;
                }

                rb.AddForce(transform.forward * 10, ForceMode.Impulse);
                if (rb.velocity.magnitude > 30)
                {
                    rb.velocity = rb.velocity.normalized * 30;
                }
                animator.SetBool("OnWalk", true);
                break;
            case 2:
                if (rotate)
                {
                    transform.Rotate(rightRotate);
                }else
                {
                    transform.Rotate(leftRotate);
                }
                break;
        }
        

        RaycastHit hit2;
        if (Physics.Raycast(this.transform.position, this.transform.forward * 5, out hit2, 5))
        {
            if(hit2.transform.tag == "Player")
            {
                Stop = rb.velocity;
                Stop.x = Stop.z = 0;
                rb.velocity = Stop;
                animator.SetBool("OnWalk", false);
                animator.SetTrigger("OnPunch");
                Invoke("SetShot", 1.0f);
            }
            Mode = 0;
            ModeTime = 1.0f;
        }
    }
    void SetShot()
    {
        Destroy(Instantiate(Resources.Load("Object/Shot") as GameObject, transform.position + transform.forward * 1, transform.localRotation), 0.1f);
        GameObject.FindGameObjectWithTag("SEManager").GetComponent<SEManager>().OnSE("CatPunch");
    }
}
