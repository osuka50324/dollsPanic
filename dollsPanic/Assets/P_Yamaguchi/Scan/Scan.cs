using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scan : MonoBehaviour {
    private int g_nTime;
	// Use this for initialization
	void Start () {
        g_nTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.P) && g_nTime == 0)
        {
            GameObject Child = Instantiate(Resources.Load("Pause", typeof(GameObject))) as GameObject;
        }
	}
}
