using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour {

    [System.Serializable]
    public struct ScriptList
    {
        public GameObject Scripts;
        public float Num;
    }
    public ScriptList[] AbilityList;
    private List<Component> ComponentList = new List<Component>();


    private Animator animator;

    // Use this for initialization
    void Start ()
    {
        animator = this.GetComponent<myBody>().Body.GetComponent<Animator>();
        animator.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void SetScript()
    {
        int Cnt = 0;
        foreach(ScriptList s in AbilityList)
        {
            ComponentList.Add(gameObject.AddComponent(System.Type.GetType(s.Scripts.name)));
            ComponentList[Cnt].SendMessage(s.Scripts.name + "Set", s.Num);
            Cnt++;
            if (s.Num == 0)
                Debug.Log("能力の値が入力されていません");
        }
    }

    public void UnSetScript()
    {
        for(int i = AbilityList.Length - 1; i >= 0; i --)
        {
            Destroy(ComponentList[i]);
        }
        ComponentList.Clear();
    }
}
