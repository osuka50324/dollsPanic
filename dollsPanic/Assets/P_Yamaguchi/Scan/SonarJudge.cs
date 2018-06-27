using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarJudge : MonoBehaviour {

	// Use this for initialization
	void Start () {

    }

    // Update is called once per frame
    void Update () {
        transform.localScale += new Vector3(0.3f, 0.0f, 0.3f);
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.transform.tag);
        if (other.transform.tag != "Object")
            return;

        GameObject.FindGameObjectWithTag("SEManager").GetComponent<SEManager>().OnSE("SonarHit");
        GameObject Child = Instantiate(Resources.Load("FireParticle", typeof(GameObject))) as GameObject;
        Child.transform.parent = other.transform;
        Child.transform.position = other.transform.position;
    }
}
