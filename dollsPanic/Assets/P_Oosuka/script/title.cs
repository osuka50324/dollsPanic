using UnityEngine;
using System.Collections;
using UnityEngine.Video;

public class title : MonoBehaviour {
    
    enum TitleState
    {
        NEW_GAME,
        CONTINUE_GAME,
        END_GAME
    };
    TitleState titleState;

    [SerializeField]
    VideoPlayer videoPlayer;
    [SerializeField]
    GameObject sprites;
    [SerializeField]
    GameObject camera;
    [SerializeField]
    SceneObject nextScene;
    [SerializeField]
    GameObject[] buttons;
    [SerializeField]
    GameObject audioManager;
    [SerializeField]
    GameObject particle;

    bool isInputOK_;

    void Start()
    {
        titleState = TitleState.CONTINUE_GAME;
        sprites.SetActive(false);
        camera.GetComponent<Animator>().enabled = false;
        isInputOK_ = false;
        particle.SetActive(false);
    }

    void Update()
    {
        if(videoPlayer.enabled)
        {
            if ((ulong)videoPlayer.frame == videoPlayer.frameCount)
            {
                videoPlayer.enabled = false;
                sprites.SetActive(true);
                camera.GetComponent<Animator>().enabled = true;
                particle.SetActive(true);

                isInputOK_ = true;
                SetColor((int)titleState);
            }
        }

        SelectMode();      
    }

    void SetColor(int index)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<SpriteRenderer>().color = Color.white;
            buttons[i].GetComponent<Animator>().Play(buttons[i].name, 0, 0.0f);
            buttons[i].GetComponent<Animator>().speed = 0.0f;
            
        }
        buttons[index].GetComponent<SpriteRenderer>().color = Color.red;
        buttons[index].GetComponent<Animator>().speed = 1.0f;
    }

    void SelectMode()
    {
        if(!isInputOK_)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            titleState--;
            if (titleState <= TitleState.NEW_GAME)
            {
                titleState = TitleState.NEW_GAME;
            }
            SetColor((int)titleState);
            audioManager.GetComponent<titleAudio>().PlayCursor();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            titleState++;
            if (titleState >= TitleState.END_GAME)
            {
                titleState = TitleState.END_GAME;
            }
            SetColor((int)titleState);
            audioManager.GetComponent<titleAudio>().PlayCursor();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            audioManager.GetComponent<titleAudio>().PlayEnter();
            StartCoroutine(EnterMode());
        }
    } 

    IEnumerator EnterMode()
    {
        // 初期値 - (待ち時間 / deltaTime)
        audioManager.GetComponent<titleAudio>().BGMvolume(0.1f - (1.0f / 60.0f));

        yield return new WaitForSeconds(1.0f);

        switch (titleState)
        {
            case TitleState.NEW_GAME:
                gameDataManager.Instance.DeleteAll();
                SceneTransition.Instance.LoadScene(nextScene);
                break;
            case TitleState.CONTINUE_GAME:
                SceneTransition.Instance.LoadScene(nextScene);
                break;
            case TitleState.END_GAME:
                Application.Quit();
                break;
        }
    }
}
