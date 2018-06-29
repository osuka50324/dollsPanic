using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour {
    public bool Player = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (Player)
        {
            if(other.tag != "Player")
            {

                GameObject.FindGameObjectWithTag("SEManager").GetComponent<SEManager>().OnSE("PunchHit");
                other.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
            }
        }else
        {
            if (other.tag != "Cat")
            {

                GameObject.FindGameObjectWithTag("SEManager").GetComponent<SEManager>().OnSE("CatPunchHit");
                other.GetComponent<Rigidbody>().AddForce(transform.forward * 10000);
            }
        }
    }
}
