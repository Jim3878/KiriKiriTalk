using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUnit : BaseDialogUnit
{
    public PauseUnit(int ID, PauseUnitFactory factory) : base(ID, factory)
    {
    }

    public override void Excute(ITypewriter typewriter)
    {
        typewriter.stateController.TransTo(new PauseState(typewriter));
    }
    
}
