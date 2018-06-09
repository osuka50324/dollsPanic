using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class OptionManager : MonoBehaviour
{
    public bool SetObject;
    // Use this for initialization
    void Start()
    {
        SetObject = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.GetChildCount() == 1)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            DrawPause();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            DrawClear();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            DrawOver();
        }
    }
    public void DrawPause()
    {
        GameObject Child = Instantiate(Resources.Load("Pause", typeof(GameObject))) as GameObject;
        Child.transform.parent = transform;
        Child.transform.GetComponent<OptionScript>().g_nMode = 0;
    }
    public void DrawClear()
    {
        GameObject Child = Instantiate(Resources.Load("Clear", typeof(GameObject))) as GameObject;
        Child.transform.parent = transform;
        Child.transform.GetComponent<OptionScript>().g_nMode = 1;
    }
    public void DrawOver()
    {
        GameObject Child = Instantiate(Resources.Load("Over", typeof(GameObject))) as GameObject;
        Child.transform.parent = transform;
        Child.transform.GetComponent<OptionScript>().g_nMode = 2;
    }
}