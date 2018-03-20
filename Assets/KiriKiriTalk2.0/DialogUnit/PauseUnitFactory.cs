using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KiriUtility;

/// <summary>
/// [waitclick]
/// </summary>
public class PauseUnitFactory : BaseDialogUnitFactory
{
    //[pause]
    protected override IDialogUnit Build(int ID, Dictionary<string, string> keyValuePairs)
    {
        return new PauseUnit(ID, this);
    }
    
    protected override bool IsValueMatch(Dictionary<string, string> keyValuePairs)
    {
        return (keyValuePairs.Count == 1) && keyValuePairs.IsValueMatch("waitclick", TypeEnum.NULL);
    }
}
