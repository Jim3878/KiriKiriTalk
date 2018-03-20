using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharOutput : IDialogUnit,IDelayable
{
    private string _dialog;
    public string dialog
    {
        get
        {
            return _dialog;
        }
    }
    private CharOutputFactory factory;
    private int _ID;
    public int ID
    {
        get
        {
            return _ID;
        }
    }

    public CharOutput(int ID, CharOutputFactory factory, string dialog)
    {
        this._ID = ID;
        this._dialog = dialog;
        this.factory = factory;
    }
    
    public void Complete(ITypewriter talk){ }

    public void Excute(ITypewriter typeWriter)
    {
        factory.AddDialog(typeWriter.textStyleManager.GetLeftStyle() + _dialog + typeWriter.textStyleManager.GetRightStyle());
    }
}
