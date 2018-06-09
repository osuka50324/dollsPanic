using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class title : MonoBehaviour {

    [SerializeField]
    SceneObject nextScene;

    public void NewGame()
    {
        // セーブデータ削除
        gameDataManager.Instance.DeleteAll();

        // シーン読み込み
        SceneManager.LoadScene(nextScene);
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(nextScene);
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
