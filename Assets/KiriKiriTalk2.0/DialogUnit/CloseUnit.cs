using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseUnit : BaseDialogUnit
{
    Transform dialogBubble;
    public CloseUnit(int ID,Transform dialogBubble, IDialogUnitFactory factory) : base(ID, factory)
    {
        this.dialogBubble = dialogBubble;
    }

    public override void Excute(ITypewriter typewriter)
    {
        dialogBubble.transform.localScale = new Vector3(0, 1);
    }
}
