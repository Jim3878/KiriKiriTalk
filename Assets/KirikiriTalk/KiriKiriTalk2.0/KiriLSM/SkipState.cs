using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipState : BaseKiriState {
    public SkipState(  KiriTalk talk) : base( talk)
    {
    }

    public override void StateBegin()
    {
        foreach (IDialogUnit unit in talk.runningDialogUnitList)
        {
            unit.Complete(talk);
        }
    }

    public override void StateExit()
    {
           
    }

    public override void StateUpdate()
    {
        var currentDialogUnit = talk.GetCurrentDialogUnit();
        currentDialogUnit.Excute(talk);
        currentDialogUnit.Complete(talk);
        talk.NextDialogUnit();
    }
}
