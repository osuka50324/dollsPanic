using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        transform.parent.parent.GetComponent<PauseScript>().GetObjectReturnSelect(this.gameObject);
    }

    // Update is called once per frame
    void Update ()
    {
        if (transform.parent.parent.GetComponent<PauseScript>().bDestroy[2])
        {
            Destroy(this);
        }

    }
}
