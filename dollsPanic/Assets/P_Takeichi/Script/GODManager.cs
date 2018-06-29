using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GODManager : MonoBehaviour
{
    public GameObject Canvas;
    Timer TimeScript;
    ImageSlide IS;
    public float g_fMaxTime;
    bool menuFlag;
    OptionScript OS;
    private GameObject Child;
    private int n_MenuFlag;
    private Ability Abi;
    private bool b_Start = false;
    private bool b_End = false;

    private bool Sousa = false;
    private GameObject SousaObj = null;

    [SerializeField]
    private int maxStage_;
    
    // Use this for initialization
    void Start()
    {
        Canvas = GameObject.Find("Canvas");
        TimeScript = Canvas.GetComponent<Timer>();
        IS = Canvas.GetComponent<ImageSlide>();
        TimeScript.SetMaxTime(g_fMaxTime);
        TimeScript.StopTimer();
        IS.StartStagingBigin();
        Invoke("NoStart",0.2f);
    }

    void NoStart()
    {
        Abi = GameObject.FindGameObjectWithTag("Player").GetComponent<Ability>();
        Abi.UnSetScript();
    }


    // Update is called once per frame
    void Update()
    {
        if (Sousa && Input.anyKeyDown)
        {
            Sousa = false;
            Destroy(SousaObj);
            SousaObj = null;
            TimeScript.StartTimer();
            Abi.SetScript();
        }
        if (Input.GetKeyDown(KeyCode.Return) && !b_Start)
        {
            b_Start = true;
            IS.StartStagingFin();
            TimeScript.StartTimer();
            Abi = GameObject.FindGameObjectWithTag("Player").GetComponent<Ability>();
            Abi.SetScript();
        }
        if(TimeScript == null && !b_End)
        {
            b_End = true;
            //時間切れ
            IS.EndStagingBigin(false);
            DrawOver();
            TimeScript.StopTimer();
            menuFlag = true;
            Abi = GameObject.FindGameObjectWithTag("Player").GetComponent<Ability>();
            Abi.UnSetScript();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GetComponent<AudioSource>().enabled = !GetComponent<AudioSource>().enabled;
        }



        if (menuFlag)
        {
            for (int i = 0; i < 4; i++)
            {
                if (OS.bDestroy[i] == true)
                {
                    switch (OS.g_nMode)
                    {
                        case 0:
                            switch (i)
                            {
                                case 0://ゲームに戻る
                                    n_MenuFlag = 1;
                                    break;
                                case 1://リトライ
                                    n_MenuFlag = 2;
                                    break;
                                case 2://ステージセレクトに遷移
                                    n_MenuFlag = 3;
                                    break;
                                case 3://ヘルプの表示
                                    n_MenuFlag = 4;
                                    break;
                            }
                            break;
                        case 1:
                            switch (i)
                            {
                                case 0://次のステージへ
                                    n_MenuFlag = 5;
                                    break;
                                case 1://リトライ
                                    n_MenuFlag = 6;
                                    break;
                                case 2://ステージセレクトへ
                                    n_MenuFlag = 7;
                                    break;
                                case 3://タイトルへ
                                    n_MenuFlag = 8;
                                    break;
                            }
                            break;
                        case 2:
                            switch (i)
                            {
                                case 0://リトライ
                                    n_MenuFlag = 9;
                                    break;
                                case 1://ヘルプの表示
                                    n_MenuFlag = 10;
                                    break;
                                case 2://ステージセレクトへ
                                    n_MenuFlag = 11;
                                    break;
                                case 3://タイトルへ
                                    n_MenuFlag = 12;
                                    break;
                            }
                            break;
                    }
                    menuFlag = false;
                    OS = null;
                }
            }
            return;
        }
        if(Child == null)
        {
            if(n_MenuFlag > 0)
            {
                switch (n_MenuFlag)
                {
                    case 1:
                        TimeScript.StartTimer();
                        Abi.SetScript();
                        break;
                    case 2://リトライ
                        SceneTransition.Instance.LoadScene(SceneManager.GetActiveScene().name);
                        break;
                    case 3://ステージセレクトに遷移
                        SceneTransition.Instance.LoadScene("stageSelect");
                        break;
                    case 4:
                        Sousa = true;
                        SousaObj = Instantiate(Resources.Load("sousa") as GameObject,Canvas.transform) as GameObject;
                        break;
                    case 5://次のステージへ
                        int stageNumber = int.Parse(SceneManager.GetActiveScene().name.ToCharArray()[5].ToString());
                        stageNumber++;
                        if(stageNumber > maxStage_)
                        {
                            break;
                        }
                        SceneTransition.Instance.LoadScene("stage" + stageNumber);
                        break;
                    case 6://リトライ
                        SceneTransition.Instance.LoadScene(SceneManager.GetActiveScene().name);
                        break;
                    case 7://ステージセレクトへ
                        SceneTransition.Instance.LoadScene("stageSelect");
                        break;
                    case 8://タイトルへ
                        SceneTransition.Instance.LoadScene("title");
                        break;
                    case 9://リトライ
                        SceneTransition.Instance.LoadScene(SceneManager.GetActiveScene().name);
                        break;
                    case 10:
                        break;
                    case 11://ステージセレクトへ
                        SceneTransition.Instance.LoadScene("stageSelect");
                        break;
                    case 12://タイトルへ
                        SceneTransition.Instance.LoadScene("title");
                        break;
                }
                n_MenuFlag = 0;
            }
        }
        if (TimeScript.g_bTimer)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                DrawPause();
                TimeScript.StopTimer();
                menuFlag = true;
                Abi = GameObject.FindGameObjectWithTag("Player").GetComponent<Ability>();
                Abi.UnSetScript();
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                DrawClear();
                TimeScript.StopTimer();
                menuFlag = true;
                Abi = GameObject.FindGameObjectWithTag("Player").GetComponent<Ability>();
                Abi.UnSetScript();
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                DrawOver();
                TimeScript.StopTimer();
                menuFlag = true;
                Abi = GameObject.FindGameObjectWithTag("Player").GetComponent<Ability>();
                Abi.UnSetScript();
            }
        }
    }

    public void GameClear()
    {
        Debug.Log("fadsfasdfas;ojfoiajgoakapokakgpakpakfpokef");
        IS.EndStagingBigin(true);
        DrawClear();
        TimeScript.StopTimer();
        menuFlag = true;
        Abi = GameObject.FindGameObjectWithTag("Player").GetComponent<Ability>();
        Abi.UnSetScript();
    }

    public void DrawPause()
    {
        Child = Instantiate(Resources.Load("Pause", typeof(GameObject)), Canvas.transform) as GameObject;
        OS = Child.transform.GetComponent<OptionScript>();
        OS.g_nMode = 0;
    }
    public void DrawClear()
    {
        GameObject.FindGameObjectWithTag("SEManager").GetComponent<SEManager>().OnSE("Clear");
        Child = Instantiate(Resources.Load("Clear", typeof(GameObject)), Canvas.transform) as GameObject;
        OS = Child.transform.GetComponent<OptionScript>();
        OS.g_nMode = 1;
    }
    public void DrawOver()
    {
        GameObject.FindGameObjectWithTag("SEManager").GetComponent<SEManager>().OnSE("NotClear");
        Child = Instantiate(Resources.Load("Over", typeof(GameObject)), Canvas.transform) as GameObject;
        OS = Child.transform.GetComponent<OptionScript>();
        OS.g_nMode = 2;
    }
}
