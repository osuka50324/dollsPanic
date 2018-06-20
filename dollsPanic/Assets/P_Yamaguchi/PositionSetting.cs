using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionSetting : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = new Vector3(transform.parent.position.x, transform.parent.position.y + transform.parent.GetComponent<BoxCollider>().bounds.size.y * 1.5f, transform.parent.position.z);
        transform.rotation = transform.parent.rotation;
    }
}
