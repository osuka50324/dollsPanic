using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonFile : SingletonMonoBehaviour<CommonFile>
{
    Sprite[] sprits;

    protected override void Awake()
    {
        DontDestroyOnLoad(this);

        sprits = new Sprite[180];
        for (int i = 0; i < 180; i++)
        {
            Texture2D temp = Resources.Load("animation/result" + i) as Texture2D;
            sprits[i] = Sprite.Create(temp, new Rect(0, 0, temp.width, temp.height), Vector2.zero);
        }
    }
	
	public Sprite GetSprite(int index)
    {
        if(sprits == null)
        {
            return null;
        }

        if(index > sprits.Length)
        {
            index = sprits.Length - 1;
        }
        return sprits[index];
    }
}
