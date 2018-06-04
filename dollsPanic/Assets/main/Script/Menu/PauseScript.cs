using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour {
    public int CurrentButtonNumber = 0;
    public int OldButtonNumber = 0;
    public GameObject[] g_PauseButton = new GameObject[4];
    public int EffectTime;
    public bool EnterButton;
    public bool[] bDestroy = new bool[4];
	// Use this for initialization
	void Start ()
    {
        CurrentButtonNumber = 0;
        OldButtonNumber = 0;
        EffectTime = 100;
        EnterButton = false;
        for (int i = 0; i < 4; i++) 
        {
            bDestroy[i] = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (EnterButton && EffectTime != 0)
        {
            g_PauseButton[CurrentButtonNumber].GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
            EffectTime--;
            if (EffectTime == 0)
            {
                Destroy(this.gameObject);
            }
            return;
        }
		if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            CurrentButtonNumber--;
            if (CurrentButtonNumber < 0)
                CurrentButtonNumber = 3;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            CurrentButtonNumber++;
            if (CurrentButtonNumber > 3)
                CurrentButtonNumber = 0;
        }
        if (OldButtonNumber != CurrentButtonNumber)
        {
            g_PauseButton[CurrentButtonNumber].GetComponent<Renderer>().material.color = Color.red;
            g_PauseButton[OldButtonNumber].GetComponent<Renderer>().material.color = Color.white;
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            EnterButton = true;
            bDestroy[CurrentButtonNumber] = true;
        }
        OldButtonNumber = CurrentButtonNumber;
    }
    public void GetObjectReturnGame(GameObject ReturnGame)
    {
        g_PauseButton[0] = ReturnGame;
        g_PauseButton[0].GetComponent<Renderer>().material.color = Color.red;
    }
    public void GetObjectRetry(GameObject Retry)
    {
        g_PauseButton[1] = Retry;
    }
    public void GetObjectReturnSelect(GameObject ReturnSelect)
    {
        g_PauseButton[2] = ReturnSelect;
    }
    public void GetObjectHelp(GameObject Help)
    {
        g_PauseButton[3] = Help;
    }
}
