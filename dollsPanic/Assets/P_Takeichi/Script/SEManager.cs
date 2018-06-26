using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour {
    public AudioClip Cancel;
    public AudioClip GoriraPunch;
    public AudioClip Jump;
    public AudioClip Clear;
    public AudioClip NotClear;
    public AudioClip Sonar;
    public AudioClip SonarHit;
    public AudioClip HighJump;
    public AudioClip PunchHit;
    public AudioClip RailGo;
    public AudioClip Cursor;
    public AudioClip CatPunchHit;
    public AudioClip Enter;
    public AudioClip HighFall;
    public AudioClip Extend;
    public AudioClip Fall;
    public AudioClip CatPunch;

    public GameObject SoundGroup;
    private SEGroup SEG;
    private List<AudioClip> SE;
	// Use this for initialization
	void Start () {
        SE.Add(Cancel);
        SE.Add(GoriraPunch);
        SE.Add(Jump);
        SE.Add(Clear);
        SE.Add(NotClear);
        SE.Add(Sonar);
        SE.Add(SonarHit);
        SE.Add(HighJump);
        SE.Add(PunchHit);
        SE.Add(RailGo);
        SE.Add(Cursor);
        SE.Add(CatPunchHit);
        SE.Add(Enter);
        SE.Add(HighFall);
        SE.Add(Extend);
        SE.Add(Fall);
        SE.Add(CatPunch);
        SEG = SoundGroup.GetComponent<SEGroup>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnSE(string[] name)
    {
        foreach (var n in SE)
        {
            if (n.name.Equals(name))
            {
                SEG.SetSE(n);
                break;
            }
        }
    }
}
