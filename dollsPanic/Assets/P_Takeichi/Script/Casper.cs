using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casper : MonoBehaviour {
    private Vector3 pos = new Vector3(0,-20,0);
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(GetComponent<Player>() == null)
        {
            this.transform.position = pos;
            this.transform.tag = "UnderObject";
        }
	}
}
