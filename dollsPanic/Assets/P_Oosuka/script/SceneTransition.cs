using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneTransition : SingletonUIGraphics<SceneTransition>
{
    [SerializeField, Range(0, 1)]
    float range;
    [SerializeField]
    float second;
    [SerializeField]
    Texture maskTexture_ = null;

    public static Texture screenShotTexture_ = null;

    protected override void Start()
    {
        if (screenShotTexture_ == null)
        {
            screenShotTexture_ = new Texture2D(Screen.width, Screen.height);
        }
        range = 1.0f;
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

#if UNITY_EDITOR
    protected override void OnValidate()
    {
        UpdateMaskCutout(range);
        UpdateMaskTexture(maskTexture_);
    }
#endif

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
}
