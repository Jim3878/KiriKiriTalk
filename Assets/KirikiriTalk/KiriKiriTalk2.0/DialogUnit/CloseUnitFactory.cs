using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KiriUtility;

public class CloseUnitFactory : BaseDialogUnitFactory
{
    Transform dialogBubble;
    public CloseUnitFactory(Transform dialogBubble)
    {
        this.dialogBubble = dialogBubble;
    }

    protected override IDialogUnit Build(int ID, Dictionary<string, string> keyValuePairs)
    {
        return new CloseUnit(ID, dialogBubble, this);
    }

    protected override bool IsValueMatch(Dictionary<string, string> keyValuePairs)
    {
        return keyValuePairs.Count == 1 &&
            keyValuePairs.IsValueMatch("hides", TypeEnum.NULL);
    }
}
