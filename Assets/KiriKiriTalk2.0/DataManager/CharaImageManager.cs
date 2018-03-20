using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaImageManager : ICharaImageManager
{
    Dictionary<string, Transform> charaImageList=new Dictionary<string, Transform>();
    public void AddChara(string name, Transform transform)
    {
        if (charaImageList.ContainsKey(name))
            throw new ArgumentException("企圖加入已存在之角色\n name = " + name);
        if (transform.GetComponent<Image>() == null)
            throw new ArgumentException("企圖加入不包含Image元件的Transform\n name = " + name);
        charaImageList.Add(name, transform);
    }

    public bool ContainsChara(string name)
    {
        return charaImageList.ContainsKey(name);
    }

    public Transform GetChara(string name)
    {
        if (!charaImageList.ContainsKey(name))
            throw new KeyNotFoundException("企圖取得不存在之角色\n name = " + name);
        return charaImageList[name];
    }
}
