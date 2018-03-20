using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseDialogUnit : IDialogUnit
{
    private int _ID;
    public int ID
    {
        get
        {
            return _ID;
        }
    }
    private IDialogUnitFactory _factory;
    protected IDialogUnitFactory factory
    {
        get {
            return _factory;
        }
    }
    
    public BaseDialogUnit(int ID,IDialogUnitFactory factory)
    {
        this._ID = ID;
        this._factory = factory;
    }

    public void Complete(ITypewriter typewriter)
    {
       
    }

    public abstract void Excute(ITypewriter typewriter);
}
