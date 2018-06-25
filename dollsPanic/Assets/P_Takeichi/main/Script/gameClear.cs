using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



/**************************************

    ����̓x�b�h�ɐG�ꂽ��ԂŃG���^�[�ŃN���A
    �{���͎q���ɐG�ꂽ��ԂŃG���^�[�ŃN���A�ɕύX����K�v������
    �X�N���v�g�̃A�^�b�`���x�b�h����q���ɕύX���邱�ƂőΉ��\

*/

public class gameClear : MonoBehaviour {

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Debug.Log(GameObject.FindGameObjectWithTag("GOD"));
        GameObject.FindGameObjectWithTag("GOD").GetComponent<GODManager>().GameClear();

        char[] cName = name.ToCharArray();
        char cName2 = cName[5];
        int stageNumber = int.Parse(cName2.ToString());

        // �X�R�A�X�V
        float highScore = gameDataManager.Instance.GetHighScore(stageNumber);

        GameObject canvas = GameObject.Find("Canvas");
        Timer timer = canvas.GetComponent<Timer>();

        float time = timer.GetCurrentTime();
        float maxTime = timer.g_fMaxTime;
        float clearTime = maxTime - time;
        if (highScore > clearTime)
        {
            highScore = clearTime;
        }

        // �Z�[�u
        gameDataManager.Instance.Save(stageNumber, highScore);
    }
}
