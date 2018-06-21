using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translucent : MonoBehaviour {
    public GameObject player;
    private Color col;
    private List<GameObject> objlist;
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

        //もしRayにオブジェクトが衝突したら
        //                  ↓Ray  ↓Rayが当たったオブジェクト ↓距離
        if (Physics.Raycast(ray, out hit, distance))
        {
            Debug.Log(hit.transform.name);
            col = hit.transform.gameObject.GetComponent<myBody>().Sukin.GetComponent<SkinnedMeshRenderer>().material.color;
            col.a = 0.4f;
            hit.transform.gameObject.GetComponent<myBody>().Sukin.GetComponent<SkinnedMeshRenderer>().material.color = col;
        }
    }

    public void SetPlayer(GameObject P)
    {
        player = P;
    }
}
