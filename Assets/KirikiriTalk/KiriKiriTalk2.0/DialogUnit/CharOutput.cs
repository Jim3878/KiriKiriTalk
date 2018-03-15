using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharOutput : IDialogUnit
{
    public string dialog;

    public CharOutput(string dialog)
    {
        this.dialog = dialog;
    }

    public void Complete(KiriTalk talk)
    {
        
    }

    public void Excute(KiriTalk kirikiriTalke)
    {
        kirikiriTalke.AddDialog(dialog);
    }
}
