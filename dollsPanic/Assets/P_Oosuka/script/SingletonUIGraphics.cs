using System;
using UnityEngine;

public class SingletonUIGraphics<T> : UnityEngine.UI.Graphic where T : UnityEngine.UI.Graphic
{
    private static T instance_;

    public static T Instance
    {
        get
        {
            if (instance_ == null)
            {
                Type t = typeof(T);

                instance_ = (T)FindObjectOfType(t);
                if (instance_ == null)
                {
                    Debug.LogError(t + " をアタッチしているGameObjectはありません");
                }
            }

            return instance_;
        }
    }

    protected override void Awake()
    {
        // 他のGameObjectにアタッチされているか調べる.
        // アタッチされている場合は破棄する.
        if (this != Instance)
        {
            Destroy(this);
            //Destroy(this.gameObject);
            Debug.LogError(
                typeof(T) +
                " は既に他のGameObjectにアタッチされているため、コンポーネントを破棄しました." +
                " アタッチされているGameObjectは " + Instance.gameObject.name + " です.");
            return;
        }
    }
}