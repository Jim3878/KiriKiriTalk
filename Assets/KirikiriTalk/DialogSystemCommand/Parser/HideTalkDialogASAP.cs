using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
namespace KirikiriTalk
{
    public class HideTalkDialogASAP : KiriParser
    {
        protected override bool IsMatch(DialogUnit order)
        {
            return order.IsHeaderEqualTo("HideS");
        }

        protected override void Parse(KirikiriController ctrl, DialogUnit order)
        {
            ctrl.talkBody.localScale = Vector3.zero;
            
        }
    }
}