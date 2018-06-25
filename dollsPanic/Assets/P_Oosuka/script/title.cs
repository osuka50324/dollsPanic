using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class title : MonoBehaviour {
    
    enum TitleState
    {
        NEW_GAME,
        CONTINUE_GAME,
        END_GAME
    };
    TitleState titleState;

    [SerializeField]
    SceneObject nextScene;

    [SerializeField]
    GameObject[] buttonImage;

    [SerializeField]
    GameObject[] titleCharacter_;

    int animationCurrentIndex_;

    void Start()
    {
        titleState = TitleState.CONTINUE_GAME;
        SetColor((int)titleState);

        for (int i = 0; i < titleCharacter_.Length; i++)
        {
            titleCharacter_[i].GetComponent<Animator>().speed = 0.0f;
        }
        animationCurrentIndex_ = 0;
        PlayAnimation();
    }

    void Update()
    {
        SelectMode();

        Animator animator = titleCharacter_[animationCurrentIndex_].GetComponent<Animator>();
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if(stateInfo.normalizedTime >= 1.0f)
        {
            animator.speed = 0.0f;

            animationCurrentIndex_++;
            if(animationCurrentIndex_ >= titleCharacter_.Length)
            {
                animationCurrentIndex_ = 0;
            }
            PlayAnimation();
        }
        
    }

    void SetColor(int index)
    {
        for (int i = 0; i < buttonImage.Length; i++)
        {
            buttonImage[i].GetComponent<Image>().color = Color.white;
        }
        buttonImage[index].GetComponent<Image>().color = Color.red;
    }

    void SelectMode()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            titleState--;
            if (titleState <= TitleState.NEW_GAME)
            {
                titleState = TitleState.NEW_GAME;
            }
            SetColor((int)titleState);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            titleState++;
            if (titleState >= TitleState.END_GAME)
            {
                titleState = TitleState.END_GAME;
            }
            SetColor((int)titleState);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
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

    void PlayAnimation()
    {
        Animator animator = titleCharacter_[animationCurrentIndex_].GetComponent<Animator>();
        string name = titleCharacter_[animationCurrentIndex_].name;

        animator.Play(name, 0, 0.0f);
        animator.speed = 1.0f;
    }
}
