using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextStyleUnit : BaseDialogUnit
{
    TextStyle style;

    public TextStyleUnit(int ID, IDialogUnitFactory factory,TextStyle style) : base(ID, factory)
    {
        this.style = style;
    }

    public override void Excute(ITypewriter typewriter)
    {
        typewriter.textStyleManager.AddStyle(style);
    }
}
