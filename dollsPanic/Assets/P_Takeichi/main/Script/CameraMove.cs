using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private GameObject player;
    private const float MoveSpeed = 10;
    private const float RotateSpeed = 10;
    private const int ChangeTime = 40;
    private string PlayerName;
    private int DeltaTime = 0;
    // Use this for initialization
    void Start()
    {
        PlayerName = player.name;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = player.transform.position;
        targetPosition += player.transform.forward * -5;
        targetPosition += player.transform.up * 2;
        if (player.name != PlayerName)
        {
            DeltaTime = ChangeTime;
        }
        if (DeltaTime > 0)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, targetPosition, MoveSpeed * (Time.deltaTime * 3.5f));
            transform.localRotation = Quaternion.Lerp(this.transform.localRotation, Quaternion.LookRotation(player.transform.position - this.transform.position), RotateSpeed * (Time.deltaTime * 3.5f));
            DeltaTime--;
            PlayerName = player.name;
            return;
        }
        transform.position = Vector3.MoveTowards(this.transform.position, targetPosition, MoveSpeed);
        transform.localRotation = Quaternion.Lerp(this.transform.localRotation, Quaternion.LookRotation(player.transform.position - this.transform.position), RotateSpeed);
        PlayerName = player.name;
        //if (transform.position.z < 0)
        //{
        //    transform.position = new Vector3(10, 2, 0);
        //}

        //if (transform.position.z >= 18)
        //{
        //    transform.position = new Vector3(10, 2, 18);
        //}
    }

    public void SetPlayer(GameObject P)
    {
        player = P;
    }

    public GameObject GetPlayer()
    {
        return player;
    }
}
