using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectCon : MonoBehaviour {

    private GameObject TimeScript;      // タイマースクリプト格納用オブジェクト
    private GameObject FailureEffect;   // 失敗時(ゲームオーバー)エフェクト


	// Use this for initialization
	void Start () {
        TimeScript = GameObject.Find("Canvas") as GameObject;               // キャンバス内のタイマースクリプト取得
        //FailureEffect = (GameObject)Resources.Load("FailureEffect"); // 失敗時エフェクト取得 現状リソースフォルダ直下
	}
	
	// Update is called once per frame
	void Update () {
		// transform.parent.position
        // ↑親の位置(ごりら？)
        //TimeScript.GetComponent<Timer>().g_fMaxTime

        // 経過時間に伴いサイズ変更
        transform.GetComponent<ParticleSystem>().startSize = TimeScript.GetComponent<Timer>().g_fCurrentTime / 
                                                             TimeScript.GetComponent<Timer>().g_fMaxTime;

        // タイマー0になったらかたまりがぼわっってでて消えちゃった演出
        // 猫にひっかかれた時もぼわって出す
        if(TimeScript.GetComponent<Timer>().g_fCurrentTime <= 0f)
        {
            GameObject FEffect = Instantiate(FailureEffect, new Vector3(transform.parent.position.x, transform.parent.GetComponent<BoxCollider>().center.y, transform.parent.position.z), Quaternion.identity);
            FEffect.GetComponent<ParticleSystem>().Play();

            if(!FEffect.GetComponent<ParticleSystem>().IsAlive())
            {
                Destroy(FEffect);
                // 画面遷移

            }
        }


        // 位置取得(乗り移ってるキャラの中心からぽわぽわ～)
        float height = transform.parent.GetComponent<BoxCollider>().center.y;
        transform.position = new Vector3(transform.parent.position.x,transform.parent.position.y + height, transform.parent.position.z);
	}
}
