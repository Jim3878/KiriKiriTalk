using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyImmediateDialogUnit : BaseDialogUnit
{
    public int count;
    public DummyImmediateDialogUnit(int ID) : base(ID, null)
    {
        count = 0;
    }

    public override void Excute(ITypewriter typewriter)
    {
        count++;
    }
}
