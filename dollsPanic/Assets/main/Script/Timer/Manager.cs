using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {
    GameObject Canvas;
    Timer script;
    public float g_fMaxTime;
    // Use this for initialization
    void Start () {
        Canvas = GameObject.Find("Canvas");
        script = Canvas.GetComponent<Timer>();
        script.SetMaxTime(g_fMaxTime);
        script.StartTimer();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Return))
        {
            script.StopTimer();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            script.StartTimer();
        }
	}
    
}


