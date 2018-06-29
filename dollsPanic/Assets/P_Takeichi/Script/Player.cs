using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private List<GameObject> TargetList = new List<GameObject>();
    private int TargetNo = 0;
    private GameObject MyArea;
    private GameObject MyEffect;
    private GameObject TargetEffect;
    private Animator animator;
    // Use this for initialization

    void Start ()
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMove>().SetPlayer(this.gameObject);
        if (transform.name == "boy")
        {
            GetComponent<Ability>().SetScript();
            return;
        }
        MyArea = Instantiate(this.gameObject, transform.position, transform.localRotation) as GameObject;
        MyArea.transform.parent = this.transform;
        MyArea.GetComponent<Collider>().isTrigger = true;
        MyArea.GetComponent<Rigidbody>().useGravity = false;
        Destroy(MyArea.GetComponent<Player>());
        Destroy(MyArea.GetComponent<Ability>());
        Destroy(MyArea.GetComponent<myBody>());
        Destroy(MyArea.GetComponent<Casper>());
        MyArea.AddComponent<PlayerArea>();

        GetComponent<Collider>().material = Resources.Load("PhysicMaterial/Player") as PhysicMaterial;
        MyEffect = Resources.Load("Effect/PlayerEffect") as GameObject;
        MyEffect = Instantiate(MyEffect) as GameObject;
        MyEffect.transform.parent = this.transform;
        MyEffect.transform.localPosition = Vector3.zero;
        MyEffect.transform.localScale = Vector3.one;
        TargetEffect = Resources.Load("Effect/Target") as GameObject;
        TargetEffect = Instantiate(TargetEffect) as GameObject;
        TargetEffect.transform.parent = this.transform;
        TargetEffect.transform.localPosition = Vector3.zero;
        TargetEffect.transform.localScale = Vector3.one;
        TargetEffect.SetActive(false);
        GetComponent<Ability>().SetScript();
        animator = this.GetComponent<myBody>().Body.GetComponent<Animator>();
        animator.enabled = true;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (TargetList.Count > 0)
            {
                GameObject.FindGameObjectWithTag("SEManager").GetComponent<SEManager>().OnSE("Extend");
                GameObject target = TargetList[TargetNo];
                TargetList.Clear();
                target.tag = "Player";
                target.AddComponent<Player>();
                transform.tag = "Object";
                animator.enabled = false;
                GetComponent<Collider>().material = null;
                GetComponent<Ability>().UnSetScript();
                Destroy(MyArea);
                MyEffect.transform.parent = null;
                Destroy(MyEffect);
                TargetEffect.transform.parent = null;
                Destroy(TargetEffect);
                Destroy(this);
            }
        }

        if (TargetList.Count > 0)
        {
            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                TargetNo++;
                if (TargetNo >= TargetList.Count)
                {
                    TargetNo = 0;
                }
                TargetEffect.transform.parent = TargetList[TargetNo].transform;
                TargetEffect.transform.localPosition = Vector3.zero;
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                TargetNo--;
                if (TargetNo <= -1)
                {
                    TargetNo = TargetList.Count - 1;
                }
                TargetEffect.transform.parent = TargetList[TargetNo].transform;
                TargetEffect.transform.localPosition = Vector3.zero;
            }
        }


    }

    public void OnSelect(GameObject target)
    {
        if (target.name == "CASPERObject")
            return;
        TargetList.Add(target);
        if(TargetList.Count == 1)
        {
            TargetEffect.SetActive(true);
            TargetEffect.transform.parent = target.transform;
            TargetEffect.transform.localPosition = Vector3.zero;
        }
    }
    public void UnSelect(GameObject target)
    {
        TargetList.RemoveAll(s => s == target);
        if (TargetList.Count <= 0)
        {
            TargetEffect.transform.parent = this.transform;
            TargetEffect.transform.localPosition = Vector3.zero;
            TargetEffect.SetActive(false);
        }
    }
}
