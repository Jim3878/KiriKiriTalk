using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
namespace KirikiriTalk
{
    public class HideTalkDialogASAP : KiriParser
    {
        protected override bool IsMatch(BreakOrder order)
        {
            return order.IsTitle("HideS");
        }

        protected override void Parse(KirikiriController ctrl, BreakOrder order)
        {
            ctrl.talkBody.localScale = Vector3.zero;
            
        }
    }
}