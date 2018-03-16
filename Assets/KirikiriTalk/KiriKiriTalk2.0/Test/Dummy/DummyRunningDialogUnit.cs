using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyRunningDialogUnit : BaseCompletableDialogUnit
{
    public DummyRunningDialogUnit(int ID) : base(ID, null)
    {
    }

    public int completeCount = 0;
    public override void InnerComplete(ITypewriter typeWriter)
    {
        completeCount++;
    }

    public int excuteCount = 0;
    protected override void InnerExcute(ITypewriter typeWriter)
    {
        excuteCount++;
    }
}
