using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KiriUtility;

/// <summary>
/// [size=20]
/// </summary>
public class SizeUnitFactory : BaseDialogUnitFactory
{
    protected override IDialogUnit Build(int ID, Dictionary<string, string> keyValuePairs)
    {
        return new TextStyleUnit(ID, this, new TextStyle("size", keyValuePairs["size"]));
    }

    protected override bool IsValueMatch(Dictionary<string, string> keyValuePairs)
    {
        return keyValuePairs.Count == 1 && keyValuePairs.IsValueMatch("size", TypeEnum.INT);
    }
}
