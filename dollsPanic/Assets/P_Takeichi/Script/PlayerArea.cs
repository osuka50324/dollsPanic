using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArea : MonoBehaviour {
    private Player PlayerScript;

	// Use this for initialization
	void Start () {
        PlayerScript =  transform.parent.GetComponent<Player>();
        Vector3 scl = transform.localScale;
        scl.x *= 1.5f;
        scl.z *= 1.5f;
        transform.localScale = scl;
        Destroy(transform.GetChild(0).gameObject);
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = transform.parent.transform.position;
	}

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Object")
        {
            PlayerScript.OnSelect(col.gameObject);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Object")
        {
            PlayerScript.UnSelect(col.gameObject);
        }
    }
}
