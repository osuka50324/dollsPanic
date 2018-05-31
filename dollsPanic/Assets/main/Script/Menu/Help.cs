using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Help : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        transform.parent.parent.GetComponent<PauseScript>().GetObjectHelp(this.gameObject);
    }

    // Update is called once per frame
    void Update ()
    {
        if (transform.parent.parent.GetComponent<PauseScript>().bDestroy[3])
        {
            Destroy(this);
        }

    }
}
