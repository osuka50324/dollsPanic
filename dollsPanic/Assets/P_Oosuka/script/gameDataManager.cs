using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameDataManager {

    private static gameDataManager instance;

    // Constructor
    private gameDataManager()
    { 
        Debug.Log("Create gameDataManager instance.");
    }

    public static gameDataManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new gameDataManager();
            }
            return instance;
        }
    }

    // �f�[�^�Z�[�u
    public void Save(int stageNumber, float time)
    {
        PlayerPrefs.SetFloat(stageNumber.ToString(), time);
    }

    // �f�[�^���[�h
    public float Load(int stageNumber)
    {
        return PlayerPrefs.GetFloat(stageNumber.ToString(),0.0f);
    }

    // �f�[�^�L�[�w��폜
    public void Delete(int stageNumber)
    {
        PlayerPrefs.DeleteKey(stageNumber.ToString());
    }

    // �f�[�^�S�폜
    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }

    // ���݂̍ō��X�R�A�擾
    public float GetHighScore(int stageNumber)
    {
        return PlayerPrefs.GetFloat(stageNumber.ToString(), 0.0f);
    }
}