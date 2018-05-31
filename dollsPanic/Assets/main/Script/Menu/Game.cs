using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        transform.parent.parent.GetComponent<PauseScript>().GetObjectReturnGame(this.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
        if (transform.parent.parent.GetComponent<PauseScript>().bDestroy[0])
        {
            Destroy(this);
        }
	}

}
