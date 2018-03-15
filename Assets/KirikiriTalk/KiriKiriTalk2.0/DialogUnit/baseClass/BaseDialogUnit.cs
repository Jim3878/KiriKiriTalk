using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseDialogUnit : IDialogUnit {
    
    public void Complete(KiriTalk talk)
    {
        InnerComplete(talk);
        talk.runningDialogUnitList.Remove(this);
    }

    public abstract void InnerComplete(KiriTalk talk);
    
    public void Excute(KiriTalk talk)
    {
        talk.runningDialogUnitList.Add(this);
        InnerExcute(talk);
    }

    protected abstract void InnerExcute(KiriTalk talk);

}
