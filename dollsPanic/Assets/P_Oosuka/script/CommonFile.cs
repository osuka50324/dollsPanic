using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonFile : SingletonMonoBehaviour<CommonFile>
{
    Sprite[,] sprits;

    protected override void Awake()
    {
        DontDestroyOnLoad(this);

        sprits = new Sprite[180,3];
        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < 180; i++)
            {
                Texture2D temp = Resources.Load("animation" + j + "/result" + i) as Texture2D;
                sprits[i,j] = Sprite.Create(temp, new Rect(0, 0, temp.width, temp.height), Vector2.zero);
            }
        }
    }

    public Sprite GetSprite(int index, int type)
    {
        if(sprits == null)
        {
            return null;
        }

        if(index > sprits.Length)
        {
            index = sprits.Length - 1;
        }
        return sprits[index, type];
    }
}
