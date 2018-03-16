using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipState : BaseKiriState
{
    public SkipState(ITypewriter typewriter) : base(typewriter)
    {
    }

    public override void StateBegin()
    {
        base.StateBegin();
        if (typewriter.runningDialogUnitManager.count > 0)
        {
            var runningDialogUnitArr = typewriter.runningDialogUnitManager.GetAllDialogUnit();
            foreach (IDialogUnit dialogUnit in runningDialogUnitArr)
            {
                typewriter.runningDialogUnitManager.RemoveDialogUnit(dialogUnit.ID);
                dialogUnit.Complete(typewriter);
            }
        }
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        //if(typewriter.)
        if (!typewriter.isTerminate &&
            !typewriter.unreadDialogUnitManager.isEmpty)
        {
            var dialogunit = typewriter.unreadDialogUnitManager.PopDialogUnit();
            dialogunit.Excute(typewriter);
            dialogunit.Complete(typewriter);
        }
    }
}
