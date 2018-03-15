using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalState : BaseKiriState
{

    float startCountTime;
    public NormalState(KiriTalk talk) : base(talk)
    {
    }

    public override void StateBegin()
    {
        startCountTime = Time.time - talk.delayTime;
    }

    public override void StateExit()
    {

    }

    public override void StateUpdate()
    {
        if (talk.isRunning && Time.time - startCountTime > talk.delayTime)
        {
            startCountTime = Time.time;
            var currentDialogUnit = talk.GetCurrentDialogUnit();
            talk.ExcuteDialogUnit(currentDialogUnit);
            talk.NextDialogUnit();
        }
    }
}
