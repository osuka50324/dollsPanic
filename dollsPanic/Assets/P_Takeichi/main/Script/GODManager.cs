using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GODManager : MonoBehaviour
{
    public GameObject Canvas;
    Timer TimeScript;
    ImageSlide IS;
    public float g_fMaxTime;
    bool menuFlag;
    OptionScript OS;
    
    // Use this for initialization
    void Start()
    {
        Canvas = GameObject.Find("Canvas");
        TimeScript = Canvas.GetComponent<Timer>();
        IS = Canvas.GetComponent<ImageSlide>();
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


        if (menuFlag)
        {
            for (int i = 0; i < 4; i++) {
                if(OS.bDestroy[i] == true)
                {
                    switch(OS.g_nMode)
                    {
                        case 0:
                            switch (i)
                            {
                                case 0://ゲームに戻る
                                    break;
                                case 1://リトライ
                                    break;
                                case 2://ステージセレクトに遷移
                                    break;
                                case 3://ヘルプの表示
                                    break;
                            }
                            break;
                        case 1:
                            switch (i)
                            {
                                case 0://次のステージへ
                                    break;
                                case 1://リトライ
                                    break;
                                case 2://ステージセレクトへ
                                    break;
                                case 3://タイトルへ
                                    break;
                            }
                            break;
                        case 2:
                            switch (i)
                            {
                                case 0://リトライ
                                    break;
                                case 1://ヘルプの表示
                                    break;
                                case 2://ステージセレクトへ
                                    break;
                                case 3://タイトルへ
                                    break;
                            }
                            break;
                    }
                }
            }
            return;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            DrawPause();
            menuFlag = true;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            DrawClear();
            menuFlag = true;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            DrawOver();
            menuFlag = true;
        }
    }

    public void DrawPause()
    {
        GameObject Child = Instantiate(Resources.Load("Pause", typeof(GameObject))) as GameObject;
        Child.transform.parent = Canvas.transform;
        OS = Child.transform.GetComponent<OptionScript>();
        OS.g_nMode = 0;
    }
    public void DrawClear()
    {
        GameObject Child = Instantiate(Resources.Load("Clear", typeof(GameObject))) as GameObject;
        Child.transform.parent = Canvas.transform;
        OS = Child.transform.GetComponent<OptionScript>();
        OS.g_nMode = 1;
    }
    public void DrawOver()
    {
        GameObject Child = Instantiate(Resources.Load("Over", typeof(GameObject))) as GameObject;
        Child.transform.parent = Canvas.transform;
        OS = Child.transform.GetComponent<OptionScript>();
        OS.g_nMode = 2;
    }
}
