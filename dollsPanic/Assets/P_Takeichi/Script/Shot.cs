﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Player" || other.tag != "Cat")
            other.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
    }
}
