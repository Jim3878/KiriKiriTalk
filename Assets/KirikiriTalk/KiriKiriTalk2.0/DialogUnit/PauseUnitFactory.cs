using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUnitFactory : BaseDialogUnitFactory
{
    protected override IDialogUnit Build(List<KeyValuePair<string, string>> Value)
    {
        return new PauseUnit();
    }

    protected override bool IsHeadMached(string header)
    {
        return header.Equals("pause");
    }

    protected override bool IsValueTypeMached(List<KeyValuePair<string, string>> value)
    {
        if (value.Count == 1)
        {
            //if (value[0].Value == null)
                return true;
        }
        return false;
    }
}
