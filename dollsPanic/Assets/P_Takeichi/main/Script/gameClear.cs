using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



/**************************************

    現状はベッドに触れた状態でエンターでクリア
    本当は子供に触れた状態でエンターでクリアに変更する必要がある
    スクリプトのアタッチをベッドから子供に変更することで対応可能

*/

public class gameClear : MonoBehaviour {

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Debug.Log(GameObject.FindGameObjectWithTag("GOD"));
        GameObject.FindGameObjectWithTag("GOD").GetComponent<GODManager>().GameClear();

        char[] cName = name.ToCharArray();
        char cName2 = cName[5];
        int stageNumber = int.Parse(cName2.ToString());

        // スコア更新
        float highScore = gameDataManager.Instance.GetHighScore(stageNumber);

        GameObject canvas = GameObject.Find("Canvas");
        Timer timer = canvas.GetComponent<Timer>();

        float time = timer.GetCurrentTime();
        float maxTime = timer.g_fMaxTime;
        float clearTime = maxTime - time;
        if (highScore > clearTime)
        {
            highScore = clearTime;
        }

        // セーブ
        gameDataManager.Instance.Save(stageNumber, highScore);
    }
}
