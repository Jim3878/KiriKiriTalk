using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using KiriUtility;

public abstract class BaseDialogUnitFactory : IDialogUnitFactory
{
    public IDialogUnit BuildDialogUnit(int ID, Dictionary<string, string> keyValuePairs)
    {
        if (!IsValueMatch(keyValuePairs))
            throw new ArgumentException("資料不符\n" + keyValuePairs.ToDebugString());
        return Build(ID, keyValuePairs);
    }

    protected abstract IDialogUnit Build(int ID, Dictionary<string, string> keyValuePairs);
    
    public bool CanBuild(Dictionary<string, string> keyValuePairs)
    {
        return IsValueMatch(keyValuePairs);
    }
    
    protected abstract bool IsValueMatch(Dictionary<string, string> keyValuePairs);
    
}