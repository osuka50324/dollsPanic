using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stageSelect : MonoBehaviour {

    //****************************************************************
    // 定義
    [System.Serializable]
    struct StageData
    {
        public SceneObject sceneObject;
        public Sprite sprite;
        public GameObject gameObject;
    }

    //****************************************************************
    // 変数
    [SerializeField]
    StageData[] stageData;
    
    int currentStageNumber;
    bool isChanging;
    int widthInterval;

    //****************************************************************
    // メソッド
	void Start ()
    {
        currentStageNumber = 0;
        widthInterval = 1080;
        isChanging = false;

        // ステージ画像生成
        CreateStageImage();

    }

	void Update ()
    {
        // 右
        for (int i = 0; i < stageData.Length; i++)
        {
            RectTransform rect = stageData[i].gameObject.GetComponent<RectTransform>();
            Vector2 position = rect.anchoredPosition;
            position.x += 5.0f;

            if(position.x > (Screen.width * 0.5f + widthInterval * 0.5f))
            {
                position.x -= widthInterval * stageData.Length;
            }

            rect.anchoredPosition = position;
        }
        /*
        // 左
        for (int i = 0; i < stageData.Length; i++)
        {
            RectTransform rect = stageData[i].gameObject.GetComponent<RectTransform>();
            Vector2 position = rect.anchoredPosition;
            position.x -= 5.0f;

            if (position.x < -(Screen.width * 0.5f + widthInterval * 0.5f))
            {
                position.x += widthInterval * stageData.Length;
            }

            rect.anchoredPosition = position;
        }
        */

        /*
		if(Input.GetKey(KeyCode.RightArrow))
        {
            currentStageNumber++;
            if(stgageScene.Length > currentStageNumber)
            {
                currentStageNumber = 0;
            }
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            currentStageNumber--;
            if (stgageScene.Length < 0)
            {
                currentStageNumber = stgageScene.Length - 1;
            }
        }
        */
    }

    void CreateStageImage()
    {
        Transform canvas = GameObject.Find("Canvas").transform;
        for (int i = 0; i < stageData.Length; i++)
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

            // 座標設定
            RectTransform rect = empty.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(widthInterval * i, 50);
            rect.sizeDelta = new Vector2(960, 540);

            // 最後だけ座標が違う
            if (stageData.Length - 1 == i)
            {
                rect.anchoredPosition = new Vector2(-widthInterval, 50);
            }

            // オブジェクトのセット
            stageData[i].gameObject = empty;
        }
    }
}
