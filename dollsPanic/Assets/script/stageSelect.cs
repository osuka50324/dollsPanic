using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stageSelect : MonoBehaviour {

    [SerializeField]
    int imageHeight;
    [SerializeField]
    int imageWidth;

    [SerializeField]
    SceneObject[] stgageScene;
    
    int currentStageNumber;


	// Use this for initialization
	void Start ()
    {
        currentStageNumber = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKey(KeyCode.RightArrow))
        {
            currentStageNumber++;
            if(stgageScene.Length > currentStageNumber)
            {
                currentStageNumber = 0;
            }
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            currentStageNumber--;
            if (stgageScene.Length < 0)
            {
                currentStageNumber = stgageScene.Length - 1;
            }
        }
    }
}
