using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextStyleManager : ITextStyleManager
{
    Dictionary<string, TextStyle> styleList = new Dictionary<string, TextStyle>();
    bool hasConvertToArr = false;
    TextStyle[] _styleArr;
    TextStyle[] styleArr
    {
        get
        {
            if (!hasConvertToArr)
            {
                hasConvertToArr = true;
                _styleArr = new TextStyle[styleList.Count];
                int index = 0;
                foreach (var stylePair in styleList)
                {
                    _styleArr[index] = stylePair.Value;
                    index++;
                }
            }
            return _styleArr;
        }
    }
    public int count
    {
        get
        {
            return styleList.Count;
        }
    }

    public void AddStyle(TextStyle style)
    {
        hasConvertToArr = false;
        RemoveStyle(style.header);
        if (style == null)
        {
            throw new ArgumentNullException("style是空的啊");
        }
        styleList.Add(style.header, style);
    }

    public void RemoveStyle(TextStyle style)
    {
        RemoveStyle(style.header);
    }

    public void RemoveStyle(string header)
    {
        hasConvertToArr = false;
        styleList.Remove(header);
    }

    public string GetLeftStyle()
    {
        string leftStyle = "";
        for (int i = 0; i < styleArr.Length; i++)
        {
            leftStyle += GetLeftStyle(i);
        }
        return leftStyle;
    }

    public string GetRightStyle()
    {
        string rightStyle = "";
        for (int i = 0; i < styleArr.Length; i++)
        {
            rightStyle += GetRightStyle(i);
        }
        return rightStyle;
    }

    public bool HasStyle(string header)
    {
        return styleList.ContainsKey(header);
    }

    public TextStyle GetStyle(string header)
    {
        if (!styleList.ContainsKey(header))
            throw new KeyNotFoundException(string.Format("head不見囉~", header));
        return styleList[header];

    }

    public string GetLeftStyle(int index)
    {
        try
        {
            return styleArr[index].leftStyle;
        }
        catch (IndexOutOfRangeException)
        {
            Debug.Log(string.Format("index爆炸了~\nindex={0}\ncount={1}", index, count));
            throw;
        }
    }

    public string GetRightStyle(int index)
    {
        try
        {
            return styleArr[count - 1 - index].rightStyle;
        }
        catch (IndexOutOfRangeException)
        {
            Debug.Log(string.Format("index爆炸了~\nindex={0}\ncount={1}", index, count));
            throw;
        }
    }
}
