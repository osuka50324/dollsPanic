using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myBody : MonoBehaviour {
    public GameObject Body;
    private Rigidbody rb;
    private bool deth = false;
    public float DethPow;
    public float MaxSpeed;
    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (rb.velocity.magnitude > DethPow)
        {
            deth = true;
            Debug.Log("しねる");
        }
    }
    void OnCollisionEnter(Collision col)
    {
        if (deth)
        {
            Debug.Log("し");
            GameObject.FindGameObjectWithTag("SEManager").GetComponent<SEManager>().OnSE("HighFall");
            Instantiate(Resources.Load("ParticleDust") as GameObject).transform.position = this.transform.position;
            GameObject obj = GameObject.FindGameObjectWithTag("UnderObject");
            obj.tag = "Player";
            obj.transform.position = this.transform.position;
            obj.transform.localRotation = this.transform.localRotation;
            obj.AddComponent<Player>();
            Destroy(this.gameObject);
        }
    }
}
