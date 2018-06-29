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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.GetComponent<Move>().MaxSpeed = this.GetComponent<Move>().MaxSpeed * 1.5f;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            this.GetComponent<Move>().MaxSpeed = this.GetComponent<myBody>().MaxSpeed;
        }
    }
}