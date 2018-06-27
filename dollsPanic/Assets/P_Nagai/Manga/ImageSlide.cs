using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ImageSlide : MonoBehaviour
{

    // public GameObject canvas;   // Canvas と Image を親子関係にするために利用

    public GameObject StartManga;       // 開始演出漫画画像
    public GameObject[] startFlame;     // コマ毎の配置

    public GameObject TrueEndManga;     // 終了クリア演出
    public GameObject BadEndManga;      // 終了失敗演出

    GameObject StartMangaImage;         // 内部で使う用の入れ物
    GameObject EndMangaImage;           // 


    float Timer = 0.0f;
    float MaxTimer = 1.0f;              // 移動しきるまでの時間


    bool bStartAlphaDown = false;       // 開始演出事前処理用フラグ
    bool bStartFinSequence = false;     // 終了処理フラグ
    bool bStartStaging = false;         // 開始演出スタートフラグ

    bool bEndFinSequence = false;       // 終了演出終了処理

    // 開始演出でのフェードインアウトフラグ
    bool bStartFadeIn = false;
    bool bStartFadeOut = false;
    // 終了演出でのフェードインアウトフラグ
    bool bEndFadeIn = false;
    bool bEndFadeOut = false;
    // フェード用アルファ値
    float alpha = 1.0f;
    float FadeSpead = 0.02f;

    // フレーム数カウント
    int nFlameCnt = 0;



    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        // デバッグ用トリガー
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Q))
            StartStagingBigin();
        if (Input.GetKeyDown(KeyCode.W))
            StartStagingFin();
        if (Input.GetKeyDown(KeyCode.E))
            EndStagingBigin(true);
        if (Input.GetKeyDown(KeyCode.R))
            EndStagingBigin(false);
        if (Input.GetKeyDown(KeyCode.T))
            EndStagingFin();
#endif
        // デバッグ用トリガー



        // データなかったら入らない(enabled とかないよって言われちゃう)
        if (StartMangaImage != null)
        {
            // 開始の流れ
            // 画像オブジェクト作成→画像表示オフ→フェードアウト(alpha値0)→画像表示オン→フェードイン(alpha値1)→移動
            // 表示オフ && フェードアウト終わっていたら
            if (StartMangaImage.GetComponent<Image>().enabled == false && !bStartFadeOut)
            {
                StartMangaImage.GetComponent<Image>().enabled = true;   // 表示オン
                bStartAlphaDown = true;                                 // 初回下用 (フェードインは基本falseの為作る前にif文に入るのを防ぐ)
                bStartFadeIn = true;                                    // フェードイン開始
            }
            if (bStartAlphaDown)
            {
                if (!bStartFadeIn)  // フェードイン終わって画像が出たら移動開始
                {
                    bStartStaging = true;
                    bStartAlphaDown = false;
                }
            }
        }
        // 終了シーケンスフラグON && フェードアウトフラグOFF時デストロイ
        if (bStartFinSequence && !bStartFadeOut)
        {
            Destroy(StartMangaImage.gameObject);
            nFlameCnt = 0;
            Timer = 0.0f;
            bStartStaging = false;
            bStartFinSequence = false;
        }



        // エンディングコマも同じやり方で。
        if (EndMangaImage != null)
        {
            // 開始の流れ
            // 画像オブジェクト作成→画像表示オフ→フェードアウト(alpha値0)→画像表示オン→フェードイン(alpha値1)
            // 表示オフ && フェードアウト終わっていたら
            if (!EndMangaImage.GetComponent<Image>().enabled && !bEndFadeOut)
            {
                EndMangaImage.GetComponent<Image>().enabled = true;   // 表示オン
                bEndFadeIn = true;                                    // フェードイン開始
            }
        }
        // 終了シーケンスフラグON && フェードアウトフラグOFF時デストロイ
        if (bEndFinSequence && !bEndFadeOut)
        {
            Destroy(EndMangaImage.gameObject);
            bEndFinSequence = false;
        }



        // フェードインアウト処理
        if (bStartFadeIn)
        {
            // フェード中にフェードアウトが呼ばれた時用
            if (!bStartFadeOut)
                StartFadeIn();
            else
            {
                bStartFadeIn = false;
                StartFadeOut();
            }
        }
        if (bStartFadeOut)
        {
            if (!bStartFadeIn)
                StartFadeOut();
            else
            {
                bStartFadeOut = false;
                StartFadeIn();
            }
        }
        if (bEndFadeIn)
        {
            if (!bEndFadeOut)
                EndFadeIn();
            else
            {
                bEndFadeIn = false;
                EndFadeOut();
            }
        }
        if (bEndFadeOut)
        {
            if (!bEndFadeIn)
                EndFadeOut();
            else
            {
                bEndFadeOut = false;
                EndFadeIn();
            }
        }



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
            if (nFlameCnt < startFlame.Length - 1)
            {
                StartMangaImage.GetComponent<RectTransform>().offsetMin = Vector2.Lerp(startFlame[nFlameCnt].GetComponent<RectTransform>().offsetMin,
                                                                                  startFlame[nFlameCnt + 1].GetComponent<RectTransform>().offsetMin, t);
                StartMangaImage.GetComponent<RectTransform>().offsetMax = Vector2.Lerp(startFlame[nFlameCnt].GetComponent<RectTransform>().offsetMax,
                                                                                  startFlame[nFlameCnt + 1].GetComponent<RectTransform>().offsetMax, t);
            }
            // 移動しきったら縮小しつつ引く
            if (nFlameCnt == startFlame.Length - 1)
            {
                StartMangaImage.GetComponent<RectTransform>().offsetMin = Vector2.Lerp(startFlame[nFlameCnt].GetComponent<RectTransform>().offsetMin, new Vector2(0.0f, 0.0f), t);
                StartMangaImage.GetComponent<RectTransform>().offsetMax = Vector2.Lerp(startFlame[nFlameCnt].GetComponent<RectTransform>().offsetMax, new Vector2(0.0f, 0.0f), t);
                if (StartMangaImage.GetComponent<RectTransform>().localScale.x > 1.0f)
                    StartMangaImage.GetComponent<RectTransform>().localScale -= new Vector3(1.4f * Time.deltaTime, 1.4f * Time.deltaTime, 0.0f);
                if (StartMangaImage.GetComponent<RectTransform>().localScale.x <= 1.0f)
                    StartMangaImage.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
            // 早送りした場合縮小が終わりきらないので
            if (nFlameCnt >= startFlame.Length)
            {
                if (StartMangaImage.GetComponent<RectTransform>().localScale.x > 1.0f)
                    StartMangaImage.GetComponent<RectTransform>().localScale -= new Vector3(1.5f * Time.deltaTime, 1.5f * Time.deltaTime, 0.0f);
                if (StartMangaImage.GetComponent<RectTransform>().localScale.x <= 1.0f)
                    StartMangaImage.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
        }   // 開始演出

    }





    //-------------------------------------- 呼び出し関数 --------------------------------------//

    //=============================================================================================
    //  開始演出スタート
    //=============================================================================================
    public void StartStagingBigin()
    {
        // 作る
        StartMangaImage = Instantiate(StartManga, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity) as GameObject;
        StartMangaImage.transform.parent = this.transform;
        StartMangaImage.transform.localScale = new Vector3(2.5f, 2.5f, 1.0f);
        // 位置調整
        StartMangaImage.GetComponent<RectTransform>().offsetMin = startFlame[nFlameCnt].GetComponent<RectTransform>().offsetMin;
        StartMangaImage.GetComponent<RectTransform>().offsetMax = startFlame[nFlameCnt].GetComponent<RectTransform>().offsetMax;
        // 表示オフ→フェードアウト
        StartMangaImage.GetComponent<Image>().enabled = false;
        bStartFadeOut = true;
    }
    //=============================================================================================
    //  開始演出エンド
    //=============================================================================================
    public void StartStagingFin()
    {
        // フェード開始フラグON
        bStartFadeOut = true;
        // 終了シーケンスフラグオン
        bStartFinSequence = true;
    }


    //=============================================================================================
    //  終了演出スタート (true : トゥルーエンド(クリア)  false : バッドエンド(失敗))
    //=============================================================================================
    public void EndStagingBigin(bool ClearCheck)
    {
        // 作る
        if (ClearCheck)  // トゥルーエンド
            EndMangaImage = Instantiate(TrueEndManga, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity) as GameObject;
        if (!ClearCheck) // バッドエンド
            EndMangaImage = Instantiate(BadEndManga, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity) as GameObject;
        EndMangaImage.transform.parent = this.transform;
        EndMangaImage.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        // 位置調整
        EndMangaImage.GetComponent<RectTransform>().offsetMin = new Vector2(0.0f, 0.0f);
        EndMangaImage.GetComponent<RectTransform>().offsetMax = new Vector2(-380.0f, 0.0f);
        // 表示オフ→フェードアウト
        EndMangaImage.GetComponent<Image>().enabled = false;
        bEndFadeOut = true;
    }
    //=============================================================================================
    //  終了演出エンド
    //=============================================================================================
    public void EndStagingFin()
    {
        // フェード開始フラグON
        bEndFadeOut = true;
        bEndFinSequence = true;
    }







    //  フェードインアウト処理
    void StartFadeIn()
    {
        alpha += FadeSpead;
        StartMangaImage.GetComponent<Image>().color = new Color(1, 1, 1, alpha);

        if (alpha >= 1)
        {
            bStartFadeIn = false;
        }
    }
    void StartFadeOut()
    {
        alpha -= FadeSpead;
        StartMangaImage.GetComponent<Image>().color = new Color(1, 1, 1, alpha);

        if (alpha <= 0)
        {
            bStartFadeOut = false;
        }
    }
    void EndFadeIn()
    {
        alpha += FadeSpead;
        EndMangaImage.GetComponent<Image>().color = new Color(1, 1, 1, alpha);

        if (alpha >= 1)
        {
            bEndFadeIn = false;
        }
    }
    void EndFadeOut()
    {
        alpha -= FadeSpead;
        EndMangaImage.GetComponent<Image>().color = new Color(1, 1, 1, alpha);

        if (alpha <= 0)
        {
            bEndFadeOut = false;
        }
    }
}
