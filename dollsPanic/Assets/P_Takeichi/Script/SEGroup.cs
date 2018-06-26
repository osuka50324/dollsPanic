using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEGroup : MonoBehaviour {
    public GameObject[] Audio;
    private int Num = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetSE(AudioClip SE)
    {
        Audio[Num].GetComponent<AudioSource>().clip = SE;
        Audio[Num].GetComponent<AudioSource>().Play();
        Num++;
        if(Num == Audio.Length)
        {
            Num = 0;
        }
    }
}
