using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseState : BaseKiriState
{
    public PauseState(  KiriTalk talk) : base( talk)
    {
    }

    public override void StateBegin()
    {
        talk.isPause = true;
    }

    public override void StateExit()
    {
        talk.isPause = false;
    }

    public override void StateUpdate()
    {
        
    }
}
