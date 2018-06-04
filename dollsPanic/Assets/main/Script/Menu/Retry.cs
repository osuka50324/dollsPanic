using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Retry : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        transform.parent.parent.GetComponent<PauseScript>().GetObjectRetry(this.gameObject);
    }

    // Update is called once per frame
    void Update ()
    {
        if (transform.parent.parent.GetComponent<PauseScript>().bDestroy[1])
        {
            Destroy(this);
        }

    }
}
