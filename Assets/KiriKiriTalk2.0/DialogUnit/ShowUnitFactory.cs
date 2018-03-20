using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KiriUtility;

public class ShowUnitFactory : BaseDialogUnitFactory
{
    Transform dialogBubble;
    public ShowUnitFactory(Transform dialogBubble)
    {
        this.dialogBubble = dialogBubble;
    }
    protected override IDialogUnit Build(int ID, Dictionary<string, string> keyValuePairs)
    {
        return new ShowUnit(ID, dialogBubble, this);
    }

    protected override bool IsValueMatch(Dictionary<string, string> keyValuePairs)
    {
        return keyValuePairs.Count == 1 &&
            keyValuePairs.IsValueMatch("show", TypeEnum.NULL);
    }
}
