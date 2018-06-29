using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pinch : MonoBehaviour {
    private GameObject Pin;
	// Use this for initialization
	void Start ()
    {
        Pin = Resources.Load("Effect/Pinch") as GameObject;
    }
	
	// Update is called once per frame
	void Update ()
    {
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Destroy(Instantiate(Pin,col.transform),0.5f);
        }
    }
}
