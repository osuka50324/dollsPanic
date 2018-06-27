using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour
{
    private Vector3 rightRotate = new Vector3(0, 5, 0);
    private Vector3 leftRotate = new Vector3(0, -5, 0);
    // Use this for initialization
    void Start () {

    }

    public void TurnSet(float Num)
    {
        rightRotate.y *= Num;
        leftRotate.y *= Num;
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(rightRotate);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(leftRotate);
        }
    }
}
