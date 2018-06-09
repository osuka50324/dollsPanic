using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ImageSlide : MonoBehaviour {

    public GameObject canvas;   // Canvas と Image を親子関係にするために利用

    public GameObject StartMangaImage;   // 開始演出漫画画像
    public GameObject[] startFlame;      // コマ毎の配置

    public GameObject EndMangaImage;     // 終了演出


    float Timer = 0.0f;
    float MaxTimer = 1.0f;              // 移動しきるまでの時間


    bool bStartStaging = false;          // 開始演出スタートフラグ

    bool bFadeInImage = false;
    bool bFadeOutImage = false;

    int nFlameCnt = 0;

    float alpha = 0.0f;
    float FadeSpead = 0.02f;


	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
        
        // フェードインアウト
        if(bFadeInImage)
        {
            FadeInImage();
        }
        if(bFadeOutImage)
        {
            FadeOutImage();
        }

        // デバッグ処理
        if (Input.GetKeyDown(KeyCode.Space))
            StartStagingBigin();
        if (Input.GetKeyDown(KeyCode.E))
            EndStagingBigin();
        if (Input.GetKey(KeyCode.S))
        {
            StartStagingFin();
        }
        if (Input.GetKey(KeyCode.Y))
            bFadeInImage = true;
        if (Input.GetKey(KeyCode.U))
            bFadeOutImage = true;



        // 開始演出スタート
        if (bStartStaging)
        {
            // タイマーカウント
            Timer += Time.deltaTime;
            // 加速
            if (Input.GetKey(KeyCode.Z))
                Timer += (Time.deltaTime * 2);

            float t = Timer / MaxTimer;
            if (Timer > MaxTimer * 2)
            {
                nFlameCnt += 1;     // フレーム数Up
                Timer = 0.0f;
                t = 0.0f;
                
            }

            // 線形補間
            if(nFlameCnt < startFlame.Length - 1)
            {
                StartMangaImage.GetComponent<RectTransform>().offsetMin = Vector2.Lerp(startFlame[nFlameCnt].GetComponent<RectTransform>().offsetMin,
                                                                                  startFlame[nFlameCnt + 1].GetComponent<RectTransform>().offsetMin, t);
                StartMangaImage.GetComponent<RectTransform>().offsetMax = Vector2.Lerp(startFlame[nFlameCnt].GetComponent<RectTransform>().offsetMax,
                                                                                  startFlame[nFlameCnt + 1].GetComponent<RectTransform>().offsetMax, t);
            }
            // 移動しきったら縮小しつつ引く
            if(nFlameCnt == startFlame.Length - 1)
            {
                StartMangaImage.GetComponent<RectTransform>().offsetMin = Vector2.Lerp(startFlame[nFlameCnt].GetComponent<RectTransform>().offsetMin, new Vector2(0.0f, 0.0f), t);
                StartMangaImage.GetComponent<RectTransform>().offsetMax = Vector2.Lerp(startFlame[nFlameCnt].GetComponent<RectTransform>().offsetMax, new Vector2(0.0f, 0.0f), t);
                if (StartMangaImage.GetComponent<RectTransform>().localScale.x > 1.0f)
                    StartMangaImage.GetComponent<RectTransform>().localScale -= new Vector3(1.0f * Time.deltaTime, 1.0f * Time.deltaTime, 0.0f);
                if (StartMangaImage.GetComponent<RectTransform>().localScale.x <= 1.0f)
                    StartMangaImage.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
            // 早送りした場合縮小が終わりきらないので
            if(nFlameCnt >= startFlame.Length)
            {
                if (StartMangaImage.GetComponent<RectTransform>().localScale.x > 1.0f)
                    StartMangaImage.GetComponent<RectTransform>().localScale -= new Vector3(1.0f * Time.deltaTime, 1.0f * Time.deltaTime, 0.0f);
                if (StartMangaImage.GetComponent<RectTransform>().localScale.x <= 1.0f)
                    StartMangaImage.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
        }   // 開始演出

        // 終了演出
        // 終了演出画像は移動させないので、フェードインアウトのみ

	}


    // スタート演出開始
    public void StartStagingBigin()
    {
        // 作る
        StartMangaImage = Instantiate(StartMangaImage, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity) as GameObject;
        StartMangaImage.transform.parent = canvas.transform;
        StartMangaImage.transform.localScale = new Vector3(2.0f, 2.0f, 1.0f);
        //StartMangaImage.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        // フェードイン
        //bFadeInImage = true;

        // アルファ値マックスになったら演出開始にしたかった
        if(StartMangaImage.GetComponent<Image>().color.a >= 1)
            bStartStaging = true;
    }
    // 開始演出終了
    public void StartStagingFin()
    { 
        // フェード開始フラグON
        //bFadeOutImage = true;

        // 終了 アルファ値0になったらデストロイしたい
        //if(StartMangaImage.GetComponent<Image>().color.a == 0)
        Destroy(StartMangaImage.gameObject);
        nFlameCnt = 0;
        bStartStaging = false;
    }


    // 終了演出開始
    public void EndStagingBigin()
    {
        // 作る
        EndMangaImage = Instantiate(EndMangaImage, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity) as GameObject;
        EndMangaImage.transform.parent = canvas.transform;
        EndMangaImage.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        // 位置調整
        EndMangaImage.GetComponent<RectTransform>().offsetMin = new Vector2(0.0f, 0.0f);
        EndMangaImage.GetComponent<RectTransform>().offsetMax = new Vector2(-500.0f, 0.0f);

        // フェードインして登場
        //bFadeInImage = true;

    }
    // 終了演出終了
    public void EndStagingFin()
    {
        // フェード開始フラグON
        // フェードアウトして終了

        // 終了
        //if(StartMangaImage.GetComponent<Image>().color.a == 0)
        Destroy(EndMangaImage.gameObject);
    }



    void FadeInImage()
    {
        alpha += FadeSpead;
        StartMangaImage.GetComponent<Image>().color = new Color(1, 1, 1, alpha);

        if (alpha >= 1)
        {
            bFadeInImage = false;
        }
    }
    void FadeOutImage()
    {
        alpha -= FadeSpead;
        StartMangaImage.GetComponent<Image>().color = new Color(1, 1, 1, alpha);

        if (alpha <= 0)
        {
            bFadeOutImage = false;
        }
    }
}
