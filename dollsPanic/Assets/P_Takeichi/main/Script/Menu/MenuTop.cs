using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTop : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        transform.parent.GetComponent<OptionScript>().GetMenuTop(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
