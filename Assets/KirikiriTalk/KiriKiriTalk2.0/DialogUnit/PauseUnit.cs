using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUnit : IDialogUnit
{
    public void Complete(KiriTalk talk)
    {
        
    }

    public void Excute(KiriTalk talk)
    {
        talk.TransTo(new PauseState(talk));
    }
    
}
