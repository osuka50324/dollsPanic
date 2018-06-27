using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : SingletonUIGraphics<SceneTransition>
{
    float range;
    [SerializeField]
    float second;
    [SerializeField]
    GameObject Image2_;
    [Tooltip("フェードが必要ならTRUE")]
    [SerializeField]
    bool doFade_;
    [SerializeField]
    Sprite doFadeSprite;
    
    Texture maskTexture_;
    int textureType = 0;
    public static Texture screenShotTexture_ = null;

    protected override void Start()
    {
        if (screenShotTexture_ == null)
        {
            screenShotTexture_ = new Texture2D(Screen.width, Screen.height);
        }

        range = 0.0f;
        if (doFade_)
        {
            range = 1.0f;
        }
        UpdateMaskCutout(range);

        textureType = Random.Range(0, 3);
        maskTexture_ = Resources.Load("animation" + textureType + "/result") as Texture2D;
        UpdateMaskTexture(maskTexture_);
        Image2_.GetComponent<Image>().sprite = doFadeSprite;
    }

    void Update()
    {
        if (range > 0.0f)
        {
            range -= Time.deltaTime / second;
            if (range <= 0.0f)
            {
                range = 0.0f;
                material.SetTexture("_ScreenShotTex", null);
            }
            UpdateMaskCutout(range);

            // 手前のテクスチャ更新
            int number = (int)((1.0f - range)* 180);
            if(number >= 180)
            {
                number = 180 - 1;
            }
            Image2_.GetComponent<Image>().sprite = CommonFile.Instance.GetSprite(number, textureType);
        }
    }

    void UpdateMaskCutout(float range)
    {
        material.SetFloat("_Range", 1 - range);
    }

    void UpdateMaskTexture(Texture texture)
    {
        material.SetTexture("_MaskTex", texture);
    }

    public void LoadScene(SceneObject scene)
    {
        StartCoroutine(LoadSceneCoroutine(scene));
    }

    IEnumerator LoadSceneCoroutine(SceneObject scene)
    {
        yield return new WaitForEndOfFrame();

        Texture2D texture2D = null;
        texture2D = new Texture2D(Screen.width, Screen.height);
        texture2D.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        texture2D.Apply();

        // テクスチャ保存
        screenShotTexture_ = (Texture)texture2D;

        material.SetTexture("_ScreenShotTex", screenShotTexture_);

        // シーン遷移
        SceneManager.LoadScene(scene);
    }

    public void LoadScene(string name)
    {
        StartCoroutine(LoadSceneCoroutine(name));
    }

    IEnumerator LoadSceneCoroutine(string name)
    {
        yield return new WaitForEndOfFrame();

        Texture2D texture2D = null;
        texture2D = new Texture2D(Screen.width, Screen.height);
        texture2D.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        texture2D.Apply();

        // テクスチャ保存
        screenShotTexture_ = (Texture)texture2D;

        material.SetTexture("_ScreenShotTex", screenShotTexture_);

        // シーン遷移
        SceneManager.LoadScene(name);
    }
}
