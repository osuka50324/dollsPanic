using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ImageSlide : MonoBehaviour {

    public GameObject canvas;   // Canvas と Image を親子関係にするために利用

    public GameObject MangaImage;   // 開始演出漫画画像
    public GameObject[] flame;      // コマ毎の配置


    float Timer = 0.0f;
    float MaxTimer = 1.0f;              // 移動しきるまでの時間


    bool bStartEffect = false;          // スタートフラグ

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
            StartProduction();
        if (Input.GetKey(KeyCode.S))
        {
            SkipProduction();
        }
        if (Input.GetKey(KeyCode.Y))
            bFadeInImage = true;
        if (Input.GetKey(KeyCode.U))
            bFadeOutImage = true;

        // 開始演出スタート
        if (bStartEffect)
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
            if(nFlameCnt < flame.Length - 1)
            {
                MangaImage.GetComponent<RectTransform>().offsetMin = Vector2.Lerp(flame[nFlameCnt].GetComponent<RectTransform>().offsetMin,
                                                                                  flame[nFlameCnt + 1].GetComponent<RectTransform>().offsetMin, t);
                MangaImage.GetComponent<RectTransform>().offsetMax = Vector2.Lerp(flame[nFlameCnt].GetComponent<RectTransform>().offsetMax,
                                                                                  flame[nFlameCnt + 1].GetComponent<RectTransform>().offsetMax, t);
            }
            // 移動しきったら縮小しつつ引く
            if(nFlameCnt == flame.Length - 1)
            {
                MangaImage.GetComponent<RectTransform>().offsetMin = Vector2.Lerp(flame[nFlameCnt].GetComponent<RectTransform>().offsetMin, new Vector2(0.0f, 0.0f), t);
                MangaImage.GetComponent<RectTransform>().offsetMax = Vector2.Lerp(flame[nFlameCnt].GetComponent<RectTransform>().offsetMax, new Vector2(0.0f, 0.0f), t);
                if (MangaImage.GetComponent<RectTransform>().localScale.x > 1.0f)
                    MangaImage.GetComponent<RectTransform>().localScale -= new Vector3(1.0f * Time.deltaTime, 1.0f * Time.deltaTime, 0.0f);
                if (MangaImage.GetComponent<RectTransform>().localScale.x <= 1.0f)
                    MangaImage.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
            // 早送りした場合縮小が終わりきらないので
            if(nFlameCnt >= flame.Length)
            {
                if (MangaImage.GetComponent<RectTransform>().localScale.x > 1.0f)
                    MangaImage.GetComponent<RectTransform>().localScale -= new Vector3(1.0f * Time.deltaTime, 1.0f * Time.deltaTime, 0.0f);
                if (MangaImage.GetComponent<RectTransform>().localScale.x <= 1.0f)
                    MangaImage.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }

        }   // 開始演出
        // 終了演出？


	}


    // スタート演出開始
    void StartProduction()
    {
        // 作る
        MangaImage = Instantiate(MangaImage, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity) as GameObject;
        MangaImage.transform.parent = canvas.transform;
        MangaImage.transform.localScale = new Vector3(2.0f, 2.0f, 1.0f);
        // フェードイン
        //bFadeInImage = true;
    
        if(MangaImage.GetComponent<Image>().color.a >= 1)
            bStartEffect = true;
    }

    // 演出スキップ (終了?)
    void SkipProduction()
    { 
        // フェード開始フラグON
        
        //bFadeOutImage = true;
        // 終了
        if(MangaImage.GetComponent<Image>().color.a == 0)
        Destroy(MangaImage.gameObject);
    }


    void FadeInImage()
    {
        alpha += FadeSpead;
        MangaImage.GetComponent<Image>().color = new Color(1, 1, 1, alpha);

        if (alpha >= 1)
        {
            bFadeInImage = false;
        }
    }
    void FadeOutImage()
    {
        alpha -= FadeSpead;
        MangaImage.GetComponent<Image>().color = new Color(1, 1, 1, alpha);

        if (alpha <= 0)
        {
            bFadeOutImage = false;
        }
    }
}
