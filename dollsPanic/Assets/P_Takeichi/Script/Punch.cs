using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour {

    private Rigidbody rb;
    private Animator animator;
    private Move move;
    private Jump jump;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        animator = this.GetComponent<myBody>().Body.GetComponent<Animator>();
        move = GetComponent<Move>();
        jump = GetComponent<Jump>();
    }
    public void PunchSet(float Num)
    {
    }

    // Update is called once per frame
    void Update () {
		if(Input.GetKeyDown(KeyCode.P) && rb.velocity.magnitude < 1)
        {
            move.enabled = false;
            jump.enabled = false;
            animator.SetTrigger("OnPunch");
            Invoke("SetShot", 1.0f);
            Invoke("SetOn", 2.3f);
        }
	}

    void SetShot()
    {
        GameObject shot = Instantiate(Resources.Load("Object/Shot") as GameObject, transform.position + transform.forward * 1, transform.localRotation);
        shot.GetComponent<Shot>().Player = true;
        Destroy(shot,0.1f);
        GameObject.FindGameObjectWithTag("SEManager").GetComponent<SEManager>().OnSE("GoriraPunch");
    }

    void SetOn()
    {
        move.enabled = true;
        jump.enabled = true;
    }
}
