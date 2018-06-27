using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighMove : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
    }

    public void HighMoveSet(float Num)
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            this.GetComponent<Move>().MaxSpeed = this.GetComponent<Move>().MaxSpeed * 1.5f;
        }

        if (Input.GetKeyUp(KeyCode.P))
        {
            this.GetComponent<Move>().MaxSpeed = this.GetComponent<myBody>().MaxSpeed;
        }
    }
}