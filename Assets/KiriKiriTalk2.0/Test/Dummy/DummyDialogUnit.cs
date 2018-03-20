using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyDialogUnit : IDialogUnit
{
    private int _ID;
    public int ID
    {
        get
        {
            return _ID;
        }
    }
    public int excuteCount = 0;
    public int completeCount = 0;
    public DummyDialogUnit(int id)
    {
        this._ID = id;
    }

    public void Complete(ITypewriter talk)
    {
        completeCount++;
    }

    public void Excute(ITypewriter talk)
    {
        excuteCount++;
    }
}
