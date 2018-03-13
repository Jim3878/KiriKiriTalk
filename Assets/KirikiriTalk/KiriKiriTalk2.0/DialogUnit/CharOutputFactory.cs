using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharOutputFactory : IDialogUnitFactory {
    public IDialogUnit BuildDialogUnit(string dialog)
    {
        return new CharOutput(dialog);
    }

    public bool CanBuild(string dialog)
    {
        return true;
    }
}
