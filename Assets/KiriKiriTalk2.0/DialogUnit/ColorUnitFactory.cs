using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KiriUtility;

/// <summary>
/// [color=#ffffff]
/// </summary>
public class ColorUnitFactory : BaseDialogUnitFactory
{
    protected override IDialogUnit Build(int ID, Dictionary<string, string> keyValuePairs)
    {
        return new TextStyleUnit(ID, this, new TextStyle(string.Format("Color"), keyValuePairs["color"]));
    }

    protected override bool IsValueMatch(Dictionary<string, string> keyValuePairs)
    {
        if (keyValuePairs.ContainsKey("color"))
        {
            if (keyValuePairs["color"].IsColorHex())
            {
                return true;
            }
        }
        return false;
    }
}
