using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GODManager : MonoBehaviour
{
    public GameObject Canvas;
    Timer TimeScript;
    public float g_fMaxTime;
    // Use this for initialization
    void Start()
    {
        Canvas = GameObject.Find("Canvas");
        TimeScript = Canvas.GetComponent<Timer>();
        TimeScript.SetMaxTime(g_fMaxTime);
        TimeScript.StartTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TimeScript.StopTimer();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            TimeScript.StartTimer();
        }
        if(TimeScript == null)
        {
            //時間切れ
        }
    }
}
