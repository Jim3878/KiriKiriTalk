using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitUnit : BaseCompletableDialogUnit
{
    float delay;

    public WaitUnit(int ID, IDialogUnitFactory factory,float delay) : base(ID, factory)
    {
        this.delay = delay;
    }

    public override void InnerComplete(ITypewriter typeWriter)
    {
        typeWriter.lastTypeTime = Time.time - typeWriter.typeDelay;
    }

    protected override void InnerExcute(ITypewriter typeWriter)
    {
        typeWriter.lastTypeTime = Time.time + delay - typeWriter.typeDelay;
    }
}
