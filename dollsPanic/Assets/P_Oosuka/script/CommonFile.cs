using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonFile : SingletonMonoBehaviour<CommonFile>
{
    Sprite[,] sprits;

    int index_;
    int type_;

    protected override void Awake()
    {
        DontDestroyOnLoad(this);
        sprits = new Sprite[180, 3];
    }

    void Start()
    {
        index_ = 0;
        type_ = 0;
        StartCoroutine(LoadAsync());
    }

    public Sprite GetSprite(int index, int type)
    {
        if (sprits == null)
        {
            return null;
        }

        if (index > sprits.Length)
        {
            index = sprits.Length - 1;
        }

        // 読み込んでなければ読み込む
        if (sprits[index,type] == null)
        {
            string pass = "animation" + type + "/result" + index;
            Texture2D temp = Resources.Load(pass) as Texture2D;
            sprits[index, type] = Sprite.Create(temp, new Rect(0, 0, temp.width, temp.height), Vector2.zero);

#if UNITY_EDITOR
            Debug.Log("動的読み込み");
#endif
        }
        
        return sprits[index, type];
    }

    private IEnumerator LoadAsync()
    {
        string path = "animation" + type_ + "/result" + index_;

        //非同期ロード開始
        ResourceRequest resourceRequest = Resources.LoadAsync<Texture2D>(path);

        //ロードが終わるまで待機(resourceRequest.progressで進捗率を確認出来る)
        while (!resourceRequest.isDone)
        {
            yield return 0;
        }

        //ロード完了、resourceRequest.assetからロードしたアセットを取得
        Texture2D temp = resourceRequest.asset as Texture2D;
        sprits[index_, type_] = Sprite.Create(temp, new Rect(0, 0, temp.width, temp.height), Vector2.zero);

        index_++;
        if (index_ >= 179)
        {
            index_ = 0;
            type_++;
            if (type_ >= 3)
            {
                yield break;
            }
        }

        StartCoroutine(LoadAsync());
    }
}
