using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour {
    public float g_fMaxTime;
    public float g_fCurrentTime;
    public bool g_bTimer = false;
    public Sprite[] numImage;
    GameObject[] image = new GameObject[7];
    // Use this for initialization
    void Awake ()
    {
        image[0] = GameObject.Find("number0");
        image[1] = GameObject.Find("number1");
        image[2] = GameObject.Find("number2");
        image[3] = GameObject.Find("number3");
        image[4] = GameObject.Find("number4");
        image[5] = GameObject.Find("number5");
        image[6] = GameObject.Find("number6");
    }

    // Update is called once per frame
    void Update()
    {
        if (!g_bTimer)
        {
            return;
        }
        int nMinute, nTenMin, nOneMin;
        int nSecond, nTenSec, nOneSec;
        int nDecimal, nOneDec, nTwoDec, nThrDec;
        g_fCurrentTime -= Time.deltaTime;
        nMinute = CalcMinute(g_fCurrentTime);
        nTenMin = nMinute / 10;
        nOneMin = nMinute - nTenMin * 10;
        nSecond = CalcSecond(g_fCurrentTime);
        nTenSec = nSecond / 10;
        nOneSec = nSecond - nTenSec * 10;
        nDecimal = CalcDecimal(g_fCurrentTime);
        nOneDec = nDecimal / 100;
        nTwoDec = (nDecimal - nOneDec * 100) / 10;
        nThrDec = nDecimal - nOneDec * 100 - nTwoDec * 10;
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                switch (i)
                {
                    case 0:
                        if (nTenMin == j)
                            image[i].GetComponent<Image>().sprite = numImage[j];
                        break;
                    case 1:
                        if (nOneMin == j)
                            image[i].GetComponent<Image>().sprite = numImage[j];
                        break;
                    case 2:
                        if (nTenSec == j)
                            image[i].GetComponent<Image>().sprite = numImage[j];
                        break;
                    case 3:
                        if (nOneSec == j)
                            image[i].GetComponent<Image>().sprite = numImage[j];
                        break;
                    case 4:
                        if (nOneDec == j)
                            image[i].GetComponent<Image>().sprite = numImage[j];
                        break;
                    case 5:
                        if (nTwoDec == j)
                            image[i].GetComponent<Image>().sprite = numImage[j];
                        break;
                    case 6:
                        if (nThrDec == j)
                            image[i].GetComponent<Image>().sprite = numImage[j];
                        break;
                }
            }
            //image[i].GetComponent<Image>().preserveAspect = true;
            //image[i].GetComponent<Image>().SetNativeSize();
            if (g_fCurrentTime <= 0.000f)
            {
                g_fCurrentTime = 0.000f;
                image[4].GetComponent<Image>().sprite = numImage[0];
                image[5].GetComponent<Image>().sprite = numImage[0];
                image[6].GetComponent<Image>().sprite = numImage[0];
                g_bTimer = false;
                Destroy(this);
            }
        }
    }
    public void SetMaxTime(float fMaxTime)
    {
        g_fCurrentTime = g_fMaxTime = fMaxTime;

    }
    public void StartTimer()
    {
        g_bTimer = true;
    }
    public void StopTimer()
    {
        g_bTimer = false;
    }
    public float GetCurrentTime()
    {
        return g_fCurrentTime;
    }
    public int CalcMinute(float fTime)
    {
        int nTime = Mathf.FloorToInt(fTime);
        nTime = nTime / 60;
        return nTime;
    }
    public int CalcSecond(float fTime)
    {
        int nTime = Mathf.FloorToInt(fTime);
        nTime = nTime % 60;
        return nTime;
    }
    public int CalcDecimal(float fTime)
    {
        int nTime = Mathf.FloorToInt(fTime *1000 - Mathf.FloorToInt(fTime) * 1000);
        return nTime;
    }
}
