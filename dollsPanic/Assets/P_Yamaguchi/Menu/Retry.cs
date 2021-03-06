﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Retry : MonoBehaviour
{
    public int g_Number;
    // Use this for initialization
    void Start()
    {
        switch(transform.parent.GetComponent<OptionScript>().g_nMode)
        {
            case 0:
                g_Number = 1;
                break;
            case 1:
            case 2:
                g_Number = 0;
                break;
        }
        
        transform.parent.GetComponent<OptionScript>().GetImageObject(this.gameObject,g_Number);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent.GetComponent<OptionScript>().bDestroy[g_Number])
        {
            Destroy(this);
        }

    }
}
