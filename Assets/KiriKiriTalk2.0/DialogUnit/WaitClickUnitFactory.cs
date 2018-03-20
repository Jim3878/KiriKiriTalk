using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KiriUtility;

public class WaitUnitFactory : IDialogUnitFactory
{
    public IDialogUnit BuildDialogUnit(int ID, Dictionary<string, string> keyValuePairs)
    {
        return new WaitUnit(ID, this, keyValuePairs["wait"].ToFloat());
    }

    public bool CanBuild(Dictionary<string, string> keyValuePairs)
    {
        return keyValuePairs.Count == 1 &&
            keyValuePairs.IsValueMatch("wait", TypeEnum.FLOAT);
    }
}
