using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class title : MonoBehaviour {

    [SerializeField]
    SceneObject nextScene;

    void Update()
    {
    }

    public void NewGame()
    {
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
