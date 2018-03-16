using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KiriUtility;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// [setchara,name="imageName",from="leftleft",to"left",duration=0.6f]
/// </summary>
public class AddCharaUnitFactory : BaseDialogUnitFactory
{
    string[] positionEnum = { "leftleft", "left", "midle", "right", "rightright" };
    private Dictionary<string, Vector3> position;
    private Transform parent;
    private GameObject imagePrefabs;
    private CharaImageManager charaImageManager;
    private Image GetImage()
    {
        return GameObject.Instantiate<GameObject>(imagePrefabs).GetComponent<Image>();
    }
    public Transform GetParent()
    {
        return parent;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="transforms">
    /// ll,l,m,r,rr
    /// </param>
    public AddCharaUnitFactory(CharaImageManager charaImageManager, GameObject imagePrefabs, Transform parent, Transform[] transforms)
    {
        if (imagePrefabs.GetComponent<Image>() == null)
            throw new ArgumentNullException("imagePrefabs沒有image元件");
        this.charaImageManager = charaImageManager;
        this.parent = parent;
        this.imagePrefabs = imagePrefabs;
        this.position = new Dictionary<string, Vector3>();
        for (int i = 0; i < positionEnum.Length; i++)
        {
            this.position.Add(positionEnum[i], transforms[i].position);
        }
    }

    public Tweener moveChara(string name, string file, string from, string to, float duration)
    {
        Image image;
        if (this.charaImageManager.ContainsChara(name))
        {
            Transform chara = this.charaImageManager.GetChara(file);
            image = chara.GetComponent<Image>();
        }
        else
        {
            image = GetImage();
            image.sprite = Resources.Load<Sprite>(file);
            image.SetNativeSize();
            image.transform.parent = parent;
            image.transform.position = GetPositionByString(from);
            this.charaImageManager.AddChara(name, image.transform);
        }
        var t = image.transform.DOMove(GetPositionByString(to), duration);
        return t;
    }

    public Vector3 GetPositionByString(string s)
    {
        if (!position.ContainsKey(s))
            new KeyNotFoundException("找不到這個位置\nstring = " + s);
        return position[s];
    }

    protected override IDialogUnit Build(int ID, Dictionary<string, string> keyValuePairs)
    {
        return new AddCharaUnit(ID,
            keyValuePairs["name"].ToStringNormalize(),
            keyValuePairs["file"].ToStringNormalize(),
            keyValuePairs["from"].ToStringNormalize(),
            keyValuePairs["to"].ToStringNormalize(),
            keyValuePairs["duration"].ToFloat(), this);
    }

    protected override bool IsValueMatch(Dictionary<string, string> keyValuePairs)
    {
        //Debug.Log(keyValuePairs.Count);
        //Debug.Log(keyValuePairs.IsValueMatch("duration", TypeEnum.FLOAT));
        //Debug.Log(keyValuePairs.IsValueMatch("name", TypeEnum.STRING));
        //Debug.Log(keyValuePairs.IsValueMatch("from", TypeEnum.STRING));
        //Debug.Log(keyValuePairs.IsValueMatch("file", TypeEnum.STRING));
        //Debug.Log(keyValuePairs.Count);
        return keyValuePairs.Count == 6 &&
        keyValuePairs.IsValueMatch("setchara", TypeEnum.NULL) &&
        keyValuePairs.IsValueMatch("duration", TypeEnum.FLOAT) &&
        keyValuePairs.IsValueMatch("name", TypeEnum.STRING) &&
        keyValuePairs.IsValueMatch("from", TypeEnum.STRING) &&
        keyValuePairs.IsValueMatch("file", TypeEnum.STRING) &&
        keyValuePairs.IsValueMatch("to", TypeEnum.STRING);
    }
}
