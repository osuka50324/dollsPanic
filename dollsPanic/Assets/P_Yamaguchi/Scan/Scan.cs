using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scan : MonoBehaviour {
    private int g_nTime;
    private int g_nMaxTime;
    private GameObject SonarJudge;
    private GameObject Sonar;
    // Use this for initialization
    void Start () {
        g_nTime = 0;
        g_nMaxTime = 300;
    }
    public void ScanSet(float Num)
    {
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.P) && g_nTime == 0)
        {
            SonarJudge = Instantiate(Resources.Load("SonarJudge", typeof(GameObject))) as GameObject;
            Sonar = Instantiate(Resources.Load("Sonar", typeof(GameObject))) as GameObject;
            Sonar.transform.parent = transform;
            SonarJudge.transform.position = transform.position;
            Sonar.transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
            g_nTime++;
        }
        if (g_nTime >= 1)
        {
            g_nTime++;
            if (g_nTime == g_nMaxTime)
            {
                g_nTime = 0;
                Destroy(SonarJudge);
                Destroy(Sonar);
            }
        }
	}
}
