﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class stageSelect : MonoBehaviour {

    //************************************
    // 定義
    [System.Serializable]
    struct StageData
    {
        public int stageNumber;
        public SceneObject sceneObject;
        public Sprite sprite;
        public GameObject gameObject;
    }

    //************************************
    // 変数
    [SerializeField]
    List<StageData> stageData;
    [SerializeField]
    GameObject[] clearTime;

    int widthInterval;
    float moveValue;

    //************************************
    // メソッド
    void Start ()
    {
        widthInterval = 1080;
        moveValue = 0.0f;

        // ステージ画像生成
        CreateStageImage();
    }

	void Update()
    {
        if (moveValue == 0.0f)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                moveValue = -widthInterval;
                StageRotation(-1);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                moveValue = widthInterval;
                StageRotation(1);
            }
            if( Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(stageData[0].sceneObject);
            }
        }
        else
        {
            Move();
        }

        //**************************************
        // Debug
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Return))
        {
            float time = Random.Range(0.0f, 1200.0f);
            gameDataManager.Instance.Save(stageData[0].stageNumber, time);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameDataManager.Instance.DeleteAll();
        }
#endif
        //***************************************
    }

    void Move()
    {
        // 進行方向 * 全体の移動距離 * 時間
        float move = Mathf.Sign(moveValue) * widthInterval * Time.deltaTime;

        // 移動量の反映
        moveValue -= move;

        // 進行方向と移動量の符号が異なれば行き過ぎ
        if (Mathf.Sign(move) != Mathf.Sign(moveValue))
        {
            moveValue = 0.0f;
            SetPositions(0);
            return;
        }

        // 移動
        for (int i = 0; i < stageData.Count; i++)
        {
            RectTransform rect = stageData[i].gameObject.GetComponent<RectTransform>();
            Vector2 position = rect.anchoredPosition;

            position.x += move;

            if (position.x > (Screen.width * 0.5f + widthInterval * 0.5f))
            {
                position.x -= widthInterval * stageData.Count;
            }
            if (position.x < -(Screen.width * 0.5f + widthInterval * 0.5f))
            {
                position.x += widthInterval * stageData.Count;
            }
            rect.anchoredPosition = position;
        }
    }

    void CreateStageImage()
    {
        Transform canvas = GameObject.Find("Canvas").transform;
        for (int i = 0; i < stageData.Count; i++)
        {
            // オブジェクト生成
            GameObject empty = new GameObject("Stage" + (i + 1));

            // レイヤー変更
            empty.layer = LayerMask.NameToLayer("UI");

            // コンポーネント設定
            Destroy(empty.GetComponent<Transform>());
            empty.AddComponent<RectTransform>();
            empty.AddComponent<CanvasRenderer>();

            // 画像設定
            Image image = empty.AddComponent<Image>();
            image.sprite = stageData[i].sprite;

            // 親設定
            empty.transform.SetParent(canvas);

            // スケール設定
            RectTransform rect = empty.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(960, 540);

            // オブジェクトのセット
            StageData tmp = stageData[i];
            tmp.gameObject = empty;
            tmp.stageNumber = i + 1;    // ステージ番号セット
            stageData[i] = tmp;
        }

        // 座標のセット
        SetPositions(0);
    }

    void SetPositions(int count)
    {
        StageRotation(count);

        for (int i = 0; i < stageData.Count - 1; i++)
        {
            stageData[i].gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(widthInterval * i, 50.0f);
        }
        stageData[stageData.Count - 1].gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(-widthInterval, 50.0f);

        DrawClearTime();
    }

    void StageRotation(int count)
    {
        int lastIndex = stageData.Count - 1;
        int addIndex = lastIndex;
        int removeIndex = 0;

        // 回転方向によって変更
        if (Mathf.Sign(count) > 0)
        {
            addIndex = 0;
            removeIndex = lastIndex;
        }

        // 実際にデータの入れ替え
        for (int i = 0; i < Mathf.Abs(count); i++)
        {
            StageData tmp = stageData[removeIndex];
            stageData.RemoveAt(removeIndex);
            stageData.Insert(addIndex, tmp);
        }
    }

    void DrawClearTime()
    {
        float time = gameDataManager.Instance.Load(stageData[0].stageNumber);      
        
        int Minute, TenMin, OneMin;
        int Second, TenSec, OneSec;
        int Decimal, OneDec, TwoDec;
        Minute = CalcMinute(time);
        TenMin = Minute / 10;
        OneMin = Minute - TenMin * 10;
        Second = CalcSecond(time);
        TenSec = Second / 10;
        OneSec = Second - TenSec * 10;
        Decimal = CalcDecimal(time);
        OneDec = Decimal / 100;
        TwoDec = (Decimal - OneDec * 100) / 10;
        
        clearTime[0].GetComponent<scoreSprite>().SetNumber(TenMin);
        clearTime[1].GetComponent<scoreSprite>().SetNumber(OneMin);
        clearTime[2].GetComponent<scoreSprite>().SetNumber(TenSec);
        clearTime[3].GetComponent<scoreSprite>().SetNumber(OneSec);
//        clearTime[4].GetComponent<scoreSprite>().SetNumber(OneDec);
//        clearTime[5].GetComponent<scoreSprite>().SetNumber(TwoDec);
        
    }
    public int CalcMinute(float time)
    {
        return Mathf.FloorToInt(time) / 60;
    }
    public int CalcSecond(float time)
    {
        return Mathf.FloorToInt(time) % 60;
    }
    public int CalcDecimal(float fTime)
    {
        return Mathf.FloorToInt(fTime * 1000 - Mathf.FloorToInt(fTime) * 1000);
    }
}