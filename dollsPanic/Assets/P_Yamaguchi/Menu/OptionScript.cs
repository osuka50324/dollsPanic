﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionScript : MonoBehaviour
{
    private int CurrentButtonNumber = 0;
    private int OldButtonNumber = 0;
    private GameObject g_MenuTop;
    private GameObject[] g_OptionButton = new GameObject[4];
    private Sprite[] g_SpriteImage = new Sprite[5];
    private int EffectTime;
    private bool EnterButton;
    public bool[] bDestroy = new bool[4];
    public int g_nMode;
    private int g_nCnt;
    private float fScaleX;
    private float fScaleY;
    private Vector3 vTopPos;
    private Camera MainCamera;
    // Use this for initialization
    void Start()
    {
        CurrentButtonNumber = 0;
        OldButtonNumber = 0;
        EffectTime = 100;
        EnterButton = false;
        vTopPos.y = Screen.height / 10.0f;
        vTopPos.x = Screen.width / 2.0f;
        vTopPos.z = 0;
        for (int i = 0; i < 4; i++)
        {
            bDestroy[i] = false;
        }
        if (g_nMode == 2)
            g_nCnt = 2;
        else
            g_nCnt = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (EnterButton && EffectTime != 0)
        {
            if (EffectTime % 5 == 0)
                g_OptionButton[CurrentButtonNumber].SetActive(!g_OptionButton[CurrentButtonNumber].active);
            EffectTime--;
            if (EffectTime == 0)
            {
                Destroy(this.gameObject);
            }
            return;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            GameObject.FindGameObjectWithTag("SEManager").GetComponent<SEManager>().OnSE("Cursor");
            CurrentButtonNumber--;
            if (CurrentButtonNumber < 0)
                CurrentButtonNumber = g_nCnt;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            GameObject.FindGameObjectWithTag("SEManager").GetComponent<SEManager>().OnSE("Cursor");
            CurrentButtonNumber++;
            if (CurrentButtonNumber > g_nCnt)
                CurrentButtonNumber = 0;
        }
        if (OldButtonNumber != CurrentButtonNumber)
        {
            g_OptionButton[CurrentButtonNumber].GetComponent<Image>().color = Color.red;
            g_OptionButton[OldButtonNumber].GetComponent<Image>().color = Color.white;
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GameObject.FindGameObjectWithTag("SEManager").GetComponent<SEManager>().OnSE("Enter");
            EnterButton = true;
            bDestroy[CurrentButtonNumber] = true;
        }
        OldButtonNumber = CurrentButtonNumber;
    }
    public void GetImageObject(GameObject Button, int Number)
    {
        g_OptionButton[Number] = Button;
        if (Number == 0)
        {
            g_OptionButton[Number].GetComponent<Image>().color = Color.red;
        }
    }
    public void GetMenuTop(GameObject Top)
    {
        g_MenuTop = Top;
    }
}
