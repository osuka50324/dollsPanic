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

    // データセーブ
    public void Save(int stageNumber, float time)
    {
        PlayerPrefs.SetFloat(stageNumber.ToString(), time);
    }

    // データロード
    public float Load(int stageNumber)
    {
        return PlayerPrefs.GetFloat(stageNumber.ToString(),0.0f);
    }

    // データキー指定削除
    public void Delete(int stageNumber)
    {
        PlayerPrefs.DeleteKey(stageNumber.ToString());
    }

    // データ全削除
    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }

    // 現在の最高スコア取得
    public float GetHighScore(int stageNumber)
    {
        return PlayerPrefs.GetFloat(stageNumber.ToString(), 0.0f);
    }
}