using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonFile : SingletonMonoBehaviour<CommonFile>
{
    Sprite[,] sprits;

    protected override void Awake()
    {
        DontDestroyOnLoad(this);

        sprits = new Sprite[180, 3];
        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < 180; i++)
            {
                string pass = "animation" + j + "/result" + i;
                StartCoroutine(LoadAsync(pass, i, j));
            }
        }
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

    private IEnumerator LoadAsync(string filePath, int index, int type)
    {
        //非同期ロード開始
        ResourceRequest resourceRequest = Resources.LoadAsync<Texture2D>(filePath);

        //ロードが終わるまで待機(resourceRequest.progressで進捗率を確認出来る)
        while (!resourceRequest.isDone)
        {
            yield return 0;
        }

        //ロード完了、resourceRequest.assetからロードしたアセットを取得
        Texture2D temp = resourceRequest.asset as Texture2D;
        sprits[index, type] = Sprite.Create(temp, new Rect(0, 0, temp.width, temp.height), Vector2.zero);
    }
}
