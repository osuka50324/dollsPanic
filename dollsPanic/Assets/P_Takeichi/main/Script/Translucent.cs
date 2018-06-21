using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translucent : MonoBehaviour {
    public GameObject player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(player == null)
        {
            player = GetComponent<CameraMove>().GetPlayer();
        }

        if(player == null)
        {
            return;
        }
        Ray ray = new Ray(transform.position, player.transform.position);

        //Rayが当たったオブジェクトの情報を入れる箱
        RaycastHit hit;

        //Rayの飛ばせる距離
        float distance = (transform.position - player.transform.position).magnitude;

        //Rayの可視化    ↓Rayの原点　　　　↓Rayの方向　　　　　　　　　↓Rayの色
        Debug.DrawLine(ray.origin, ray.direction * distance, Color.red);

        //もしRayにオブジェクトが衝突したら
        //                  ↓Ray  ↓Rayが当たったオブジェクト ↓距離
        if (Physics.Raycast(ray, out hit, distance))
        {
                Debug.Log("RayがPlayerに当たった");
        }
    }

    public void SetPlayer(GameObject P)
    {
        player = P;
    }
}
