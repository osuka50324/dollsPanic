﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



/**************************************

    現状はベッドに触れた状態でエンターでクリア
    本当は子供に触れた状態でエンターでクリアに変更する必要がある
    スクリプトのアタッチをベッドから子供に変更することで対応可能

*/

public class gameClear : MonoBehaviour {

    [SerializeField]
    SceneObject nextScene;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionStay()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            string name = SceneManager.GetActiveScene().name;
            char[] cName = name.ToCharArray();
            char cName2 = cName[5];
            int stageNumber = int.Parse(cName2.ToString());

            // スコア更新
            float highScore = gameDataManager.Instance.GetHighScore(stageNumber);

            float time = GameObject.Find("Canvas").GetComponent<Timer>().GetCurrentTime();
            float maxTime = GameObject.Find("Canvas").GetComponent<Timer>().g_fMaxTime;
            float clearTime = maxTime - time;
            if(highScore > clearTime)
            {
                highScore = clearTime;
            }

            // セーブ
            gameDataManager.Instance.Save(stageNumber, highScore);
            
            // 遷移
            SceneManager.LoadScene(nextScene);
        }
    }
}
