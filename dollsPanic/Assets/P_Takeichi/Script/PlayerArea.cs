using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArea : MonoBehaviour {
    private Player PlayerScript;

	// Use this for initialization
	void Start () {
        PlayerScript =  transform.parent.GetComponent<Player>();
        transform.localScale *= 1.5f;
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
