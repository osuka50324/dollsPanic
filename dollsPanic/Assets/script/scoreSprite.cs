using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreSprite : MonoBehaviour
{
    Sprite[] sprites;

    void Awake()
    {
        sprites = new Sprite[10];
        for (int i = 0; i < 10; i++)
        {
            string str = "number" + i;
            sprites[i] = Resources.Load(str, typeof(Sprite)) as Sprite;
        }
        SetNumber(0);
    }

    public void SetNumber(int number)
    {
        GetComponent<Image>().sprite = sprites[number];
    }
}