using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KiriUtility;

public class NormalState : BaseKiriState
{
    float typeDelay
    {
        get
        {
            return (1 / typewriter.typeSpeed);
        }
    }
    public NormalState(ITypewriter typewriter) : base(typewriter)
    {
    }

    public override void StateBegin()
    {
        base.StateBegin();
        typewriter.lastTypeTime = Time.time - typeDelay;
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        //Debug.Log(string.Format("terminate = {0}\nunread count = {1}\n lastTypeTime = {2}",typewriter.isTerminate, typewriter.unreadDialogUnitManager.Count, typewriter.lastTypeTime));
        if (!typewriter.isTerminate &&
            !typewriter.unreadDialogUnitManager.isEmpty &&
            Time.time - typewriter.lastTypeTime >= typeDelay)
        {
            //從新計時
            typewriter.lastTypeTime = Time.time - (Time.time - typewriter.lastTypeTime) % typeDelay;

            //執行不需等待的Units
            ExcuteAllUnitWithoutDelay();

            //執行需要等待的Units，如果有的話
            if (!typewriter.unreadDialogUnitManager.isEmpty)
            {
                var dialogunit = typewriter.unreadDialogUnitManager.PopDialogUnit();
                ExcuteUnit(dialogunit);
            }
        }
    }

    public void ExcuteAllUnitWithoutDelay()
    {
        var units = typewriter.CatchNoDelayUnit();
        for (int i = 0; i < units.Length; i++)
        {
            ExcuteUnit(units[i]);
        }
    }

    public void ExcuteUnit(IDialogUnit unit)
    {
        unit.Excute(typewriter);
    }

}
