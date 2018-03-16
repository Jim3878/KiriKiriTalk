using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCompletableDialogUnit : IDialogUnit
{

    protected IDialogUnitFactory factory;
    private int _ID;
    public int ID
    {
        get
        {
            return _ID;
        }
    }

    public BaseCompletableDialogUnit(int ID, IDialogUnitFactory factory)
    {
        this._ID = ID;
        this.factory = factory;
    }

    public void Complete(ITypewriter typeWriter)
    {
        InnerComplete(typeWriter);
        RemoveFromRunning(typeWriter);
    }

    protected void RemoveFromRunning(ITypewriter typeWriter)
    {
        typeWriter.runningDialogUnitManager.RemoveDialogUnit(ID);
    }

    public abstract void InnerComplete(ITypewriter typeWriter);

    public void Excute(ITypewriter typeWriter)
    {
        typeWriter.runningDialogUnitManager.AddDialogUnit(this);
        InnerExcute(typeWriter);
       
    }
    protected abstract void InnerExcute(ITypewriter typeWriter);

}
