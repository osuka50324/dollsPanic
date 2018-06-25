using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonar : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        GetComponent<ParticleSystem>().Emit(1000);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
