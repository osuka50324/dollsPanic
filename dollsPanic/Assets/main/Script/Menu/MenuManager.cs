using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {
    public GameObject CanvasPause;
    public bool SetObject;
	// Use this for initialization
	void Start () {
        SetObject = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (PauseButtonDetection() && transform.GetChildCount() == 0)
        {
            GameObject Child = Instantiate(CanvasPause) as GameObject;
            Child.transform.parent = transform;
        }
	}

    bool PauseButtonDetection()
    {
        if (Input.GetKeyDown(KeyCode.M))
            return true;
        return false;
    }
}
