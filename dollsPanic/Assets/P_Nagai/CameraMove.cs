﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private GameObject player;
    private const float MoveSpeed = 10;
    private const float RotateSpeed = 10;
    private float DilayTime;
    private Vector3 targetPosition;

    private bool Goal = false;


    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!Goal)
        {
            targetPosition = player.transform.position;
            targetPosition += player.transform.forward * -5;
            targetPosition += player.transform.up * 6;

            while (false)
            {
                Ray ray = new Ray(targetPosition, player.transform.position);

                //Rayが当たったオブジェクトの情報を入れる箱
                RaycastHit hit;

                //Rayの飛ばせる距離
                float distance = (targetPosition - player.transform.position).magnitude - 1.0f;

                //もしRayにオブジェクトが衝突したら
                //                  ↓Ray  ↓Rayが当たったオブジェクト ↓距離
                if (Physics.Raycast(player.transform.position, targetPosition - player.transform.position, out hit, distance))
                {
                    // プレイヤーとのレイは省く
                    if (hit.collider.tag == "Player")
                    {
                        break;
                    }
                    targetPosition += player.transform.forward;
                    targetPosition += player.transform.up;
                    DilayTime = 1.0f;
                }
                else
                {
                    break;
                }
            }
            Vector3 PPos = player.transform.position;
            PPos.y += 4;
            if (DilayTime > 0)
            {
                transform.position = Vector3.MoveTowards(this.transform.position, targetPosition, MoveSpeed * (Time.deltaTime * 3.5f));
                transform.localRotation = Quaternion.Lerp(this.transform.localRotation, Quaternion.LookRotation(PPos - this.transform.position), RotateSpeed * (Time.deltaTime * 3.5f));
                DilayTime -= Time.deltaTime;
                return;
            }
            // 場所反映
            transform.position = Vector3.MoveTowards(this.transform.position, targetPosition, MoveSpeed);
            transform.localRotation = Quaternion.Lerp(this.transform.localRotation, Quaternion.LookRotation(PPos - this.transform.position), RotateSpeed);

            //if (transform.position.z < 0)
            //{
            //    transform.position = new Vector3(10, 2, 0);
            //}

            //if (transform.position.z >= 18)
            //{
            //    transform.position = new Vector3(10, 2, 18);
            //}
        }else
        {

            if (DilayTime > 0)
            {
                transform.position = Vector3.MoveTowards(this.transform.position, targetPosition, MoveSpeed * (Time.deltaTime * 3.5f));
                transform.localRotation = Quaternion.Lerp(this.transform.localRotation, Quaternion.LookRotation(player.transform.position - this.transform.position), RotateSpeed * (Time.deltaTime * 3.5f));
                DilayTime -= Time.deltaTime;
                return;
            }
            transform.position = Vector3.MoveTowards(this.transform.position, targetPosition, MoveSpeed);
        }

    }



    public void SetPlayer(GameObject P)
    {
        player = P;
        DilayTime = 1.0f;
        if (P.name.Equals("boy"))
        {
            Goal = true;
            targetPosition = P.transform.position + P.GetComponent<GoalCamera>().pos;
        }
    }
}
